using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace AMCM
{
    class ModemInfo
    {
        private const string IpAdress = "192.168.1.1";
        //const int HttpPort = 80;
        private const int TelnetPort = 23;

        
        private Socket TlnSocket = null;

        private string RespString;

        private enum GreetingInfo { None, DLink, Qtech, Q1040_B1 };
        private enum DlinkAnswer{ None, T1A_1027, T1A_1028};

        private ModemInfoStruct MIS;





        //      ######     ##      ##   #######     ##         ########    ######
        //      #######    ##      ##   ########    ##         ########   ########
        //      ##    ##   ##      ##   ##     ##   ##            ##      ##     ##
        //      ##    ##   ##      ##   ##     ##   ##            ##      ##
        //      ##    ##   ##      ##   ########    ##            ##      ##
        //      #######    ##      ##   #######     ##            ##      ##
        //      ######     ##      ##   ##     ##   ##            ##      ##
        //      ##         ##      ##   ##     ##   ##            ##      ##     ##
        //      ##         ###    ###   ########    ########   ########   ########
        //      ##           ######     #######     ########   ########    ######
        public ModemInfoStruct GetModemInfo()
        {
            MIS = new ModemInfoStruct();
            MIS.ModemNum = -1;
            MIS.SoftNum = -1;

            Connect();

            return MIS;
        }







        //      ######     #######     ########   ##         ##      ###      ############  ##########
        //      #######    ########    ########   ##         ##     ## ##     ############  ##########
        //      ##    ##   ##     ##      ##       ##       ##     ##   ##         ##       ##
        //      ##    ##   ##     ##      ##       ##       ##     ##   ##         ##       ##
        //      ##    ##   ##    ##       ##        ##     ##     ##     ##        ##       ########
        //      #######    #######        ##        ##     ##     #########        ##       #######
        //      ######     ####           ##         ##   ##     ###########       ##       ##
        //      ##         ##  ##         ##         ##   ##     ##       ##       ##       ##
        //      ##         ##    ##    ########       ## ##     ##         ##      ##       #########
        //      ##         ##     ##   ########        ###      ##         ##      ##       ##########
        private void Connect()
        {
            // создаем новый сокет для телнета
            if (TlnSocket == null)
                TlnSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // пытаемся заТелнениться
            //this.statBar.Text = "Подлкючаемся...";
            if (TryConnectTelnet() == false)
            {
                MessageBox.Show("Не удалось подключиться.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // закрываем телнет сокет и осовобождаем ресурсы
                CloseTelnet();
                return;
            }


            // получить приглашение на ввод логина
            if (PleaseLogIn() == false)
            {
                MessageBox.Show("Превышен таймаут ожидания.","Ошибка",  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // закрываем телнет сокет и осовобождаем ресурсы
                CloseTelnet();
                return;
            }


            // анализ приветствия модема
            GreetingInfo inf = GreetingAnalise();
            if (inf == GreetingInfo.Q1040_B1)
                MIS.ModemNum = 0;


            // авторизация
            if (inf == GreetingInfo.Q1040_B1)
            {
                MIS.ModemNum = 0;
                if (AutorizQtech() == false)
                {
                    MessageBox.Show("Не удалось авторизоваться","Ошибка",  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // закрываем телнет сокет и осовобождаем ресурсы
                    CloseTelnet();
                    return;
                }
                MIS.SoftNum = GetSoftIndexQTech();

            }
            else if (inf == GreetingInfo.Qtech)
            {
                if (AutorizQtech() == false)
                {
                    MessageBox.Show("Не удалось авторизоваться","Ошибка",  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CloseTelnet();
                    return;
                }
                MIS.SoftNum = GetSoftIndexQTech();
            }
            else    // D-Link и прочие
            {
                if (AutorizDlink() == false)
                {
                    MessageBox.Show("Не удалось авторизоваться","Ошибка",  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CloseTelnet();
                    return;
                }
                
                // читаем с паузами
                int Ln = RespString.Length;
                int Ln2 = Ln;
                do
                {
                    Ln = Ln2;
                    Thread.Sleep(100);
                    RespString += ReadResponseBuffer();
                    Ln2 = RespString.Length;
                } while (Ln2 > Ln);

                // анализ чего ответил после авторизации
                if (RespString.IndexOf("NAME=DSL-2540U") > 0 ||
                    RespString.IndexOf("NAME=DSL_2540") > 0)
                {
                    MIS.ModemNum = 1;
                    if (RespString.IndexOf("VERSION=1.0.27") > 0)
                        MIS.SoftNum = 0;
                    else if (RespString.IndexOf("VERSION=1.0.28") > 0)
                        MIS.SoftNum = 1;
                    else if (RespString.IndexOf("VERSION=1.0.30") > 0)
                        MIS.SoftNum = 2;
                    else
                        MIS.SoftNum = -1;   // неизвестная прошивка
                }
                else if (RespString.IndexOf("NAME=DSL-2500U") > 0)
                {
                    MIS.ModemNum = 2;
                    if (RespString.IndexOf("VERSION=1.0.47") > 0)
                        MIS.SoftNum = 0;
                    else if (RespString.IndexOf("VERSION=1.0.49") > 0)
                        MIS.SoftNum = 1;
                    else
                        MIS.SoftNum = -1;   // неизвестная прошивка
                }
                
                else if (RespString.IndexOf("NAME=DSL-2520U") > 0)
                {
                    MIS.ModemNum = 3;
                    if (RespString.IndexOf("VERSION=1.0.4") > 0)
                        MIS.SoftNum = 0;
                    else
                        MIS.SoftNum = -1;   // неизвестная прошивка
                }
                else
                {
                    // неизвестный модем
                }

                //MIS.SoftNum = GetSoftIndex();
            }


            CloseTelnet();


        }

        

       

        // индекс прошивки QTech
        private int GetSoftIndexQTech()
        {
            // swversion в виде массива байт
            byte[] swversion = Encoding.Default.GetBytes("swversion\r\n");

            // отправляем комманду
            TlnSocket.Send(swversion);

            RespString = ReadResponseBuffer();
            if (RespString.IndexOf("QDSL-1040 V") < 0)
            {
                DateTime StartTime = DateTime.Now;
                TimeSpan TS;
                bool ok = false;
                do
                {
                    RespString += ReadResponseBuffer();
                    if (RespString.IndexOf("QDSL-1040 V") >= 0)
                    {
                        ok = true;
                        break;
                    }
                    TS = DateTime.Now - StartTime;
                }
                while (TS.Milliseconds < 500);
                if (ok == false)
                    return -1;
            }

            if (RespString.IndexOf("2.3") >= 0)
                return 0;
            else if (RespString.IndexOf("2.5") >= 0)
                return 1;

            return -1;
        }



        // анализ приветствия модема
        private GreetingInfo GreetingAnalise()
        {
            GreetingInfo rezult = GreetingInfo.None;
            if (RespString.IndexOf("QDSL") > 0)
            {
                rezult = GreetingInfo.Qtech;
                if (RespString.IndexOf("QDSL-1040rev.B1") > 0)
                    rezult = GreetingInfo.Q1040_B1;

                return rezult;
            }


            return rezult;
        }



        // получить приглашение авторизоваться
        private bool PleaseLogIn()
        {
            // что там ответило
            RespString = ReadResponseBuffer();
            if (RespString.IndexOf("Login:") > 0)
                return true;

            // если с первого раза не получилось
            DateTime DT_Start = DateTime.Now;
            TimeSpan TS;
            do
            {
                RespString += ReadResponseBuffer();
                if (RespString.IndexOf("Login:") > 0)
                    return true;
                TS = DateTime.Now - DT_Start;
            }
            while (TS.Milliseconds < 3000); // пока не прошло 2 секунды
            
            return false;
        }



        //  ПОПЫТКА СОЕДИНЕНИЯ ТЕЛНЕТА  =====================================
        private bool TryConnectTelnet()
        {
            // количество попыток подключиться к модему
            byte TryConnectCount = 0;


            // кол-во попыток подключения
            //byte MaxConnectCount = 2;
            byte MaxConnectCount = 1;

            // пробуем подключиться MaxConnectCount раз
            while (TryConnectCount < MaxConnectCount)
            {
                try
                {
                    // подключаемся 
                    TlnSocket.Connect(IpAdress, TelnetPort);    //аж 20 секунд !!!!
                    if (TlnSocket.Connected)
                    {
                        return (true);
                    }
                    else
                        TryConnectCount += 1;
                }
                // исключение возникает при неудачной попытке подключится
                catch
                {
                    TryConnectCount += 1;
                }
                //Thread.Sleep(100); он и так ждет при "TlnSocket.Connect"
            }

            return (false);
        }


        // Закрыть соединение
        private void CloseTelnet()
        {
            if (TlnSocket != null)
            {
                try
                {
                    TlnSocket.Close();
                }
                catch
                {

                }

                TlnSocket = null;
            }
        }


        // читать ответ от модема
        private string ReadResponseBuffer()
        {
            // буфер для считанных байт
            byte[] buffer = new byte[1024];
            // счетчик считанных байт
            int bytes;
            // строка
            string resp = "";

            // трохе подождем, вдруг модем еще не очухался
            Thread.Sleep(100);

            // повторяем цикл, пока есть байты в буфере
            while (TlnSocket.Available > 0)
            {
                // читаем байты в наш буфер
                bytes = TlnSocket.Receive(buffer, buffer.Length, SocketFlags.None);

                // преобразуем в строку и добавляем к предыдущей
                resp += Encoding.ASCII.GetString(buffer, 0, bytes);

                // трохе подождем, может еще чего модемс в буффер выбросит
                Thread.Sleep(100);
            }

            return resp;
        }


        // авторизация для QTech
        private bool AutorizQtech()
        {
            // admin в виде массива байт
            byte[] admin = Encoding.Default.GetBytes("admin\r\n");
            // password в виде массива байт
            byte[] passw = Encoding.Default.GetBytes("password\r\n");


            // вводим Логин
            TlnSocket.Send(admin);
            
            // ждем приглашения ввести пароль
            RespString = ReadResponseBuffer();
            if (RespString.IndexOf("Password:") < 0)
            {
                DateTime StartTime = DateTime.Now;
                TimeSpan TS;
                bool ok = false;
                do
                {
                    RespString += ReadResponseBuffer();
                    if (RespString.IndexOf("Password:") >= 0)
                    {
                        ok = true;
                        break;
                    }
                    TS = DateTime.Now - StartTime;
                } while (TS.Milliseconds < 1000);
                
                if (ok == false)
                    return false;
            }

            // трохэ ждем
            //Thread.Sleep(100);

            // вводим пароль
            TlnSocket.Send(passw);

            // трохэ ждем
            //Thread.Sleep(100);

            // читаем ответ
            RespString += ReadResponseBuffer();

            if (RespString.IndexOf("incorrect") > 0)
            {
                // попробовать админ
                

                // вводим Логин
                TlnSocket.Send(admin);

                // ждем приглашения ввести пароль
                RespString = ReadResponseBuffer();
                if (RespString.IndexOf("Password:") < 0)
                {
                    DateTime StartTime = DateTime.Now;
                    TimeSpan TS;
                    bool ok = false;
                    do
                    {
                        RespString += ReadResponseBuffer();
                        if (RespString.IndexOf("Password:") >= 0)
                        {
                            ok = true;
                            break;
                        }
                        TS = DateTime.Now - StartTime;
                    } while (TS.Milliseconds < 1000);

                    if (ok == false)
                        return false;
                }

                // трохэ ждем
                //Thread.Sleep(100);

                // вводим пароль
                TlnSocket.Send(admin);

                // трохэ ждем
                //Thread.Sleep(100);

                // читаем ответ
                RespString += ReadResponseBuffer();

                if (RespString.IndexOf("incorrect") > 0)
                    return false;
            }

            // что там осталось в буффере
            //RespString += ReadResponseBuffer();

                //bytes = TlnSocket.Receive(buffer, buffer.Length, SocketFlags.None);
                //resp += Encoding.ASCII.GetString(buffer, 0, bytes);




                return true;
        }


        // авторизация для D-Link
        private bool AutorizDlink()
        {
            // admin в виде массива байт
            byte[] admin = Encoding.Default.GetBytes("admin\r\n");
            

            

            // вводим Логин
            TlnSocket.Send(admin);

            // ждем приглашения ввести пароль
            RespString = ReadResponseBuffer();
            if (RespString.IndexOf("Password:") < 0)
            {
                DateTime StartTime = DateTime.Now;
                TimeSpan TS;
                bool ok = false;
                do
                {
                    RespString += ReadResponseBuffer();
                    if (RespString.IndexOf("Password:") < 0)
                    {
                        ok = true;
                        break;
                    }
                    TS = DateTime.Now - StartTime;
                } while (TS.Milliseconds < 1000);

                if (ok == false)
                    return false;
            }

            // трохэ ждем
            //Thread.Sleep(100);

            // вводим пароль
            TlnSocket.Send(admin);

            // трохэ ждем
            //Thread.Sleep(100);

            // читаем ответ
            RespString = ReadResponseBuffer();

            if (RespString.IndexOf("incorrect") > 0 || RespString.IndexOf("failed") > 0)
            {
                return false;
            }

            return true;
        }
        
         
    }
}


//      ###   #   #  ###   #    ###    ###
//      #  #  #   #  #  #  #     #    #   
//      ###   #   #  ###   #     #    #
//      #     #   #  #  #  #     #    #   
//      #      ####  ###   #### ###    ###



//     ###   ###   ###  #   #    #    #####  ####
//     #  #  #  #   #   #   #   # #     #    #
//     ###   ###    #    # #    ###     #    ###
//     #     # #    #    # #   #   #    #    #
//     #     #  #  ###    #    #   #    #    ####

