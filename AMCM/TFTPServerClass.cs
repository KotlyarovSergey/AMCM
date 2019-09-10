using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace AMCM
{
    class TFTPServerClass
    {
        #region Данные
        public bool IsRunned;

        private string WorkDirectory;
        private IPAddress Ip_Address;

        private UdpClient UDP;
        private IPEndPoint RemoteIpEndPoint;
 
        private enum PackageType : short { RRQ = 1, WRQ = 2, DATA = 3, ACK = 4, ERR = 5 };
        private enum ERROR_CODE : short
        {
            Unknown_Error, File_Not_Found, Access_Denied,
            Impossible_Allocate_Disk_Space, Incorrect_Operation,
            Incorrect_Transfer_ID, File_Already_Exists, User_Not_Exists,
            Incorrect_Option
        }
        private enum FileTransfer_MODE { netascii, octet };

        private const string ASCCII_MODE = "netascii";
        private const string OCTET_MODE = "octet";

        private enum Operations { Listen, ReadFile, WriteFile };
        //private Operations CurrentAction;

        private FileStream FS;
        private ushort BlockCount;
        private ushort BlocksNumber;
        #endregion


        /// <summary>
        /// Контруктор
        /// </summary>
        /// <param name="WorkDir"></param>
        public TFTPServerClass(string WorkDir, IPAddress IPadr)
        {
            //mainForm = main_Form;
            WorkDirectory = WorkDir;
            Ip_Address = IPadr;
            IsRunned = false;

            //ReadTimer = new System.Windows.Forms.Timer();
            //ReadTimer.Interval = 50;
            //ReadTimer.Tick += new EventHandler(ReadTimer_Tick);
        }


        #region PUBLIC
        /// <summary>
        /// Запуск сервера
        /// </summary>
        public void Start()
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 69);
            //IPEndPoint RemoteIpEndPoint = new IPEndPoint(Ip_Address, 69);
            try
            {
                //UDP = new UdpClient(69);
                UDP = new UdpClient(RemoteIpEndPoint);
            }
            catch
            {
                return;
            }
            
            IsRunned = true;
            //CurrentAction = Operations.Listen;
            Listener();
            UDP.Close();
        }


        /// <summary>
        /// Остановка сервера
        /// </summary>
        public void Stop()
        {
            IsRunned = false;
            //LstThread.Abort();
            try
            {
                UDP.Close();
            }
            catch
            {
            }

            UDP = null;
            
        }
        #endregion


        #region PRIVATE
        # region Слушатель порта
        private void Listener()
        {
            while (IsRunned)
            //while (true)
            {
                if (UDP.Available > 0)
                {
                    byte[] Data;
                    try
                    {
                        // получить данные из буффера
                        Data = UDP.Receive(ref RemoteIpEndPoint);
                        // анализировать их
                        DateAnalise(Data);
                    }
                    catch
                    {
                        UDP.Close();
                        IsRunned = false;
                        //LstThread.Abort();
                    }
                }
            }
        }
        #endregion


        #region анализ датаграммы
        private void DateAnalise(byte[] Data)
        {
            int len = Data.Length;
            if (len < 2)
            {
                Send_Error(ERROR_CODE.Unknown_Error);
                return;
            }

            // тип пакета
            PackageType pkType = (PackageType)((short)(Data[0] * 0x100) + Data[1]);

            // что за тип
            switch (pkType)
            {
                case PackageType.RRQ:       // Запрос на чтение
                    RRQ_Analise(Data);
                    break;

                case PackageType.WRQ:       // Запрос на запись

                    break;

                case PackageType.DATA:      // Данные

                    break;

                case PackageType.ACK:       // Подтверждение пакета
                    ACK_Analise(Data);
                    break;

                case PackageType.ERR:       // Ошибка

                    break;
            }

        }
        #endregion 


        #region анализ запроса на чтение
        private void RRQ_Analise(byte[] Data)
        {
            // начиная с 3-го байта должно идити имя файла
            int len = Data.Length;
            if (len < 4)
                return;
            
            // счетчик символов строки
            int count = 0;
            for (int i = 2; i < len; i++)
            {
                if (Data[i] == 0) // знак конца строки
                {
                    count = i - 2;
                    break;
                }
            }
            
            // имя файла
            string fName = Encoding.ASCII.GetString(Data, 2, count);
            string FullFileName = WorkDirectory + "\\" + fName;
            // существует ли файл
            if (File.Exists(FullFileName) == true)
            {
                // тип передачи
                if (count + 2 + 5 < len)
                {
                    int c = 0;
                    for (int i = count + 3; i < len; i++)
                    {
                        if (Data[i] == 0)
                            break;
                        c++;
                    }

                    string mode_str = Encoding.ASCII.GetString(Data, count + 3, c);
                    if (mode_str.CompareTo(OCTET_MODE) == 0)
                    {
                        //CurrentAction = Operations.ReadFile;
                        Send_Data(FullFileName, FileTransfer_MODE.octet);
                    }
                    else if (mode_str.CompareTo(ASCCII_MODE) == 0)
                    {
                        //CurrentAction = Operations.ReadFile;
                        Send_Data(FullFileName, FileTransfer_MODE.netascii);
                    }
                    else
                        Send_Error(ERROR_CODE.Incorrect_Option);
                }
                else    // символов меньше чем надо для "octet"
                {
                    Send_Error(ERROR_CODE.Incorrect_Option);
                }
            }
            else    // файла нет
            {
                // отослать клиенту что файла нет
                Send_Error(ERROR_CODE.File_Not_Found);
            }
        }

        #endregion

        
        #region анализ подтверждения пакета
        private void ACK_Analise(byte[] Data)
        {
            if (Data.Length < 4)
            {
                Send_Error(ERROR_CODE.Incorrect_Transfer_ID);
                return;
            }

            ushort N = (ushort)((ushort)(0x100 * Data[2]) + (ushort)Data[3]);
            if (N > BlockCount) // если указанный блок больше текущего это ошибка
            {
                Send_Error(ERROR_CODE.Incorrect_Transfer_ID);
                return;
            }
            else
            {
                if (N == BlockCount)        // номер соответствует текущему
                {
                    BlockCount++;           // отослать следующий
                    if (BlockCount > BlocksNumber)  
                    {
                        FS.Close();         // файл закончился
                        //CurrentAction = Operations.Listen;
                        return;
                    }
                }
                else                        // номер в подвтверждении меньше текущего
                    BlockCount = N;         // надо повторить
                
                SendNextBlock();
            }
        }
        #endregion


        #region отослать данные
        private void Send_Data(string FileName, FileTransfer_MODE mode)
        {
            //FileStream FS;
            try
            {
                FS = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            }
            catch
            {
                Send_Error(ERROR_CODE.Access_Denied);
                return;
            }

            // определить количество блоков
            long N = FS.Length / 512;   // так получается количество полных (по 512) блоков
            if ((N * 512) < FS.Length)
                N++;                    // плюс один для неполного
            if (N > 0xFFFF)
            {
                Send_Error(ERROR_CODE.Unknown_Error);
                FS.Close();
            }
            else
                BlocksNumber = (ushort)N;
            
            BlockCount = 1;            
            
            // отослать следующий блок
            SendNextBlock();
        }
        #endregion


        #region отослать очередной блок
        private void SendNextBlock()
        {
            // массив для отправки
            byte[] data = new byte[2 + 2 + 512];
            //byte[] data = new byte[512];
            
            // тип пакета
            byte[] PkType_Buf = BitConverter.GetBytes((short)PackageType.DATA);
            Array.Reverse(PkType_Buf);
            Array.Copy(PkType_Buf, 0, data, 0, 2);
            
            // номер блока
            byte[] BlockNumber_Buf = BitConverter.GetBytes(BlockCount);
            Array.Reverse(BlockNumber_Buf);
            Array.Copy(BlockNumber_Buf, 0, data, 2, 2);

            
            // байты файла
            int count = 512;
            long point = (BlockCount-1) * 512;
            long ostatok = FS.Length - point;
            if (ostatok < 512)
                count = (int)ostatok;
            /*
            int count = 508;
            long point = (BlockCount - 1) * 508;
            long ostatok = FS.Length - point;
            if (ostatok < 508)
                count = (int)ostatok;
            */

            FS.Seek(point, SeekOrigin.Begin);
            FS.Read(data, 4, count);
            

            try
            {
                int I = UDP.Send(data, count + 4, RemoteIpEndPoint);
            }
            catch
            {
                
            }

        }
        #endregion


        #region отослать ошибку
        private void Send_Error(ERROR_CODE ErrCode)
        {
            //
            string ASCII_Text_Description = string.Empty;
            
            switch (ErrCode)
            {
                case ERROR_CODE.Unknown_Error:
                    break;

                case ERROR_CODE.File_Not_Found:             // файл не найден
                    ASCII_Text_Description = "File Not Found";
                    break;

                case ERROR_CODE.Incorrect_Option:
                    ASCII_Text_Description = "Incorrect_Options";
                    break;

                default:
                    ASCII_Text_Description = "Unknown error";
                    break;
            }

            byte[] DataGram;
            // DataGram = PackageType(2) + ERROR_CODE(2) + ASCII_Text_descript(n) + NULL(1) 
            byte[] PkType_Buf = BitConverter.GetBytes((short)PackageType.ERR);
            Array.Reverse(PkType_Buf);
            byte[] ErrCode_Buf = BitConverter.GetBytes((short)ErrCode);
            Array.Reverse(ErrCode_Buf);
            byte[] TextDescr_Buf = Encoding.ASCII.GetBytes(ASCII_Text_Description);

            DataGram = new byte[5 + TextDescr_Buf.Length];
            Array.Copy(PkType_Buf, DataGram, 2);
            Array.Copy(ErrCode_Buf, 0, DataGram, 2, 2);
            Array.Copy(TextDescr_Buf, 0, DataGram, 4, TextDescr_Buf.Length);
            DataGram[DataGram.Length - 1] = 0;

            try
            {
                int I = UDP.Send(DataGram, DataGram.Length, RemoteIpEndPoint);
            }
            catch
            {
                
            }
        }
        #endregion


        /*
        private void ReadTimer_Tick(object sender, EventArgs e)
        {
            //ReadTimer.Stop();
            //ReadBuffer();
            //ReadTimer.Start();
        }
        */

        #endregion


    }
}
