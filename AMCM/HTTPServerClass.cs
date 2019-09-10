using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace AMCM
{

    //===============================================  КЛИЕНТ  ===============================================
    // Класс-обработчик клиента
    class Client
    {
        //public string FilePath;// = "D:\\conf.xml";
        public void strt()
        {

        }

        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        //public Client(TcpClient Client, string FilePath)
        //public void Clientt(TcpClient Client, string FilePath)
        public void Clientt(TcpClient Client, string RootFolder)
        {
            // Объявим строку, в которой будет хранится запрос клиента
            string Request = "";
            // Буфер для хранения принятых от клиента данных
            byte[] Buffer = new byte[1024];
            // Переменная для хранения количества байт, принятых от клиента
            int Count;
            // Читаем из потока клиента до тех пор, пока от него поступают данные
            while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
            {
                // Преобразуем эти данные в строку и добавим ее к переменной Request
                Request += Encoding.ASCII.GetString(Buffer, 0, Count);
                // Запрос должен обрываться последовательностью \r\n\r\n
                // Либо обрываем прием данных сами, если длина строки Request превышает 4 килобайта
                // Нам не нужно получать данные из POST-запроса (и т. п.), а обычный запрос
                // по идее не должен быть больше 4 килобайт
                if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096)
                {
                    break;
                }

                // трохе подождем
                System.Threading.Thread.Sleep(100);
            }


            string FilePath = RootFolder + "\\" + ExtactFileName(Request);


            // Если в папке www не существует данного файла, посылаем ошибку 404
            if (!File.Exists(FilePath))
            {
                //SendError(Client, 404);
                return;
            }

            // Открываем файл, страхуясь на случай ошибки
            FileStream FS;
            try
            {
                FS = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }

            catch (Exception)
            {
                // Если случилась ошибка, посылаем клиенту ошибку 500
                //SendError(Client, 500);
                return;
            }

            // размер файла
            long fsize = FS.Length;


            // Посылаем заголовки
            string Headers = "HTTP/1.0 200 OK\r\n" +
                             "Content-Length: " + fsize.ToString() + "\r\n" +
                             "Content-Disposition: attachment\r\n" +
                             "\r\n";
            byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
            Client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);

            // Пока не достигнут конец файла
            while (FS.Position < FS.Length)
            {
                // Читаем данные из файла
                Count = FS.Read(Buffer, 0, Buffer.Length);
                // И передаем их клиенту
                Client.GetStream().Write(Buffer, 0, Count);
            }

            // Закроем файл и соединение
            FS.Close();
            Client.Close();
        }

        // извлечь имя файла из запроса
        private string ExtactFileName(string Reqest)
        {
            string rezult = string.Empty;

            int P1 = Reqest.IndexOf("//");
            if (P1 > 0)
            {
                string mid = Reqest.Substring(P1 + 2);
                
                int P2 = mid.IndexOf("\r\n");
                string str;
                if (P2 > 0)
                    str = mid.Substring(0, P2);
                else
                    str = mid;

                int P3 = str.LastIndexOf(" ");
                if (P3 > 0)
                {
                    string fl = str.Substring(0, P3);
                    rezult = fl.Replace('/', '\\');
                }
            }

            return rezult;
        }

    }



    //===============================================  СЕРВЕР  ===============================================
    class Server
    {
        //public string FileName;
        public string IP;
        TcpListener Listener; // Объект, принимающий TCP-клиентов

        // Запуск сервера
        //public Server(int Port, string fn)
        public Server(int Port, string RootDir)
        {
            bool donext = false;
            string IPAddrSmall = "192.168.1.";
            string IPAddrFull = "";
            int octet = 2;
            while (donext == false)
            {
                donext = true;
                try
                {
                    IPAddrFull = IPAddrSmall + octet.ToString();
                    //IPAddress IpAdr = System.Net.IPAddress.Parse("192.168.1.3"); // IP-Address
                    IPAddress IpAdr = System.Net.IPAddress.Parse(IPAddrFull); // IP-Address
                    Listener = new TcpListener(IpAdr, Port); // Создаем "слушателя" для указанного порта
                    Listener.Start(); // Запускаем его
                }
                catch
                {
                    //Listener = null;
                    if (octet < 254)
                    {
                        IPAddrFull = "";
                        octet += 1;
                        donext = false;
                    }
                    else                  // если уж адреса с 3 по 30 заняты знач херня какаято
                    {
                        IP = "";
                        break;
                    }
                }
            }

            IP = IPAddrFull;

            // создаем экземпляр клинента
            Client cln = new Client();
            // создаем поток для обработки клиента
            Thread clnth = new Thread(new ThreadStart(delegate()
              {
                  cln.Clientt(Listener.AcceptTcpClient(), RootDir);
              }));
            // запускаем клинета
            clnth.Start();
        }

        ~Server()
        {
            // Если "слушатель" был создан
            if (Listener != null)
            {
                // Остановим его
                Listener.Stop();
            }
        }


        public string Start(string RootDir)
        {
            Server srv = new Server(80, RootDir);
            return (srv.IP);
        }

    }









    class HTTPServer
    {

        // запустиь сервер для раздачи файла FileName
        public string Run(string RootDir)
        {
            Server srv = new Server(80, RootDir);

            string s = srv.IP;

            return (s);
        }
    }
}
