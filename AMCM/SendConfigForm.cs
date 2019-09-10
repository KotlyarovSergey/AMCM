using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace AMCM
{
    public partial class SendConfigForm : Form
    {
        #region PublicValue
        //public string Modem;
        //public string SoftVer;
        public int ModemInd;
        public int SoftVerInd;
        public string IPServer;
        public string ConfFile;
        public string WorkDir;
        public string GetConfCommand;
        #endregion

        private const string IpAdress = "192.168.1.1";
        //const int HttpPort = 80;
        private const int TelnetPort = 23;

        #region PrivateValue
        private Socket TlnSocket = null;
        private string RespString;
        //private Process ServerProcess;
        Thread ServerThread;
        #endregion


        public SendConfigForm()
        {
            InitializeComponent();
        }

        private void SendConfigForm_Load(object sender, EventArgs e)
        {
            ModemList ML = new ModemList();
            List<string> LS = ML.GetModemsList();
            lblModem.Text = LS[ModemInd];

            LS = ML.GetSoftList(ModemInd);
            lblSoft.Text = LS[SoftVerInd];

            TransferTyte tft = ML.GetTansferType(ModemInd, SoftVerInd);
            if (tft == TransferTyte.http)
                cmbProtocol.SelectedIndex = 1;
            else if (tft == TransferTyte.tftp)
                cmbProtocol.SelectedIndex = 0;
            else
                cmbProtocol.SelectedIndex = -1;

            
        }

        private void cmbProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProtocol.SelectedIndex == 0) //tftp
            {
                txbServIp.Enabled = true;
                txbCommand.Enabled = true;
                btnRun.Enabled = true;
                btnReboot.Enabled = false;
                GetIpAddress();
                if (ServerThread != null)
                    ServerThread.Abort();
            }
            
            else if (cmbProtocol.SelectedIndex == 1)
            {
                txbServIp.Enabled = false;
                txbCommand.Enabled = false;
                btnRun.Enabled = false;
                btnReboot.Enabled = false;
                HTTPServer srv = new HTTPServer();
                // адрес на ктором поднялся сервер
                //string IPAddress = srv.Run(WorkDir+"\\"+ConfFile);
                string IPAddress = srv.Run(WorkDir);
                if (IPAddress.Length > 0)
                {
                    txbServIp.Enabled = true;
                    txbServIp.Text = IPAddress;
                    txbCommand.Enabled = true;
                    btnRun.Enabled = true;
                    btnReboot.Enabled = false;
                }
                else
                    MessageBox.Show("Не удалость запустить HTTP-Сервер", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // определить IP
        private void GetIpAddress()
        {
            String strHostName = Dns.GetHostName();
            //IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);
            //int nIP = 0;
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                //MessageBox.Show("IP #" + ++nIP + ": " + ipaddress.ToString());
                string ip = ipaddress.ToString();
                if (ip.IndexOf("192.168.1.") == 0)
                {
                    txbServIp.Text = ip;
                    break;
                }
            }
        }
        
        /*
        // запустить сервер
        private void btnRunServer_Click(object sender, EventArgs e)
        {
            if (cmbProtocol.SelectedIndex == 0)
            {
                string srvFileName = @"C:\Program Files\Tftpd32\tftpd32.exe";
                ServerProcess = Process.Start(srvFileName);
            }

            txbServIp.Enabled = true;
            txbServIp.Text = "192.168.1.3";

            //CreateCommandLine();

            txbCommand.Enabled = true;
            btnRun.Enabled = true;
            btnReboot.Enabled = true;
        }
        */

        // изменение IP-адреса
        private void txbServIp_TextChanged(object sender, EventArgs e)
        {
            IPServer = txbServIp.Text;
            CreateCommandLine();
        }
        
        
        // создать строку комманды
        private void CreateCommandLine()
        {
            ModemList ML = new ModemList();
            txbCommand.Text = ML.CommonFileComand(ModemInd, SoftVerInd, ConfFile, IPServer);

        }


        // изменение текста комманды
        private void txbCommand_TextChanged(object sender, EventArgs e)
        {
            GetConfCommand = txbCommand.Text;
        }


        // влить
        private void btnRun_Click(object sender, EventArgs e)
        {
            cmbProtocol.BackColor = SystemColors.Control;
            txbCommand.BackColor = SystemColors.Control;
            txbServIp.BackColor = SystemColors.Control;
            this.Enabled = false;


            // запустить сервер
            if (cmbProtocol.SelectedIndex == 0)     // TFTP
            {
                TFTPServerClass TFTP = new TFTPServerClass(WorkDir, IPAddress.Parse(IPServer));
                ServerThread = new Thread(TFTP.Start);
                ServerThread.Start();
            }
            else                                    // HTTP
            {
                // уже должен быть запущен
            }

            // законнектиться
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

            bool needCloseTlnSock = true;

            // получить приглашение на ввод логина
            if (PleaseLogIn() == false)
            {
                MessageBox.Show("Превышен таймаут ожидания.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // закрываем телнет сокет и осовобождаем ресурсы
                CloseTelnet();
                return;
            }

            // залогиниться
            bool aut;
            if (ModemInd == 0)  // Qtech
                aut = AutorizQtech();
            else
                aut = AutorizDlink();

            if (aut == true)
            {

                Thread.Sleep(500);
                RespString = ReadResponseBuffer();
                // ввести комманду
                byte[] cmd = Encoding.Default.GetBytes(GetConfCommand + "\r\n");
                TlnSocket.Send(cmd);

                Thread.Sleep(500);
                // что там ответило
                //RespString = ReadResponseBuffer();
                //if (RespString.Length == 0)
               // {
                    DateTime DTNow = DateTime.Now;
                    TimeSpan TS;
                    do
                    {
                        Thread.Sleep(100);
                        //Thread.Sleep(1000);
                        TS = DateTime.Now - DTNow;
                        RespString += ReadResponseBuffer();
                        
                        // анализ ответа
                        if (RespString.IndexOf("Got image") >= 0)
                        {
                            MessageBox.Show("Конфигурация отправлена в модем.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Thread.Sleep(1000); // время на загрузку конфа
                            break;
                        }
                        else if (RespString.IndexOf("OK <12>") >= 0)
                        {
                            MessageBox.Show("Конфигурация отправлена.\r\nНеобходимо перезагрузить модем.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            needCloseTlnSock = false;
                            btnReboot.Enabled = true;
                            break;
                        }
                            /*
                        else
                        {
                            btnReboot.Enabled = false;
                            MessageBox.Show("Ошибка!\r\nКонфигурация не отправлена!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                             */
                        if (TS.Milliseconds >= 2000)
                        {
                            MessageBox.Show("Превышен таймаут ожидания.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    //} while (RespString.Length == 0 && TS.Milliseconds < 2000);
                    } while (true);
                    
                //}

                

            }
            else
                MessageBox.Show("Ошибка авторизации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            

            // закрываем телнет сокет и осовобождаем ресурсы
            if (needCloseTlnSock)
                CloseTelnet();

            if (ServerThread != null)
                ServerThread.Abort();

            
            cmbProtocol.BackColor = SystemColors.Window;
            txbCommand.BackColor = SystemColors.Window;
            txbServIp.BackColor = SystemColors.Window;
            this.Enabled = true;
        }



        //  ПОПЫТКА СОЕДИНЕНИЯ ТЕЛНЕТА  
        private bool TryConnectTelnet()
        {
            // количество попыток подключиться к модему
            int TryConnectCount = 0;

            // пробуем подключиться 2 раза
            while (TryConnectCount < 2)
            {
                try
                {
                    // подключаемся 
                    TlnSocket.Connect(IpAdress, TelnetPort);
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


        // перезагрузить модем
        private void btnReboot_Click(object sender, EventArgs e)
        {
            byte[] reboot = Encoding.Default.GetBytes("reboot\r\n");
            TlnSocket.Send(reboot);
            Thread.Sleep(200);
            
            //TlnSocket.Close();
            CloseTelnet();
            
            this.Close();
        }


        // перед закрытием формы
        private void SendConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ServerThread != null)
                ServerThread.Abort();
        }
        
        
    }
}
