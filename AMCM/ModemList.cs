using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMCM
{
    class ModemList
    {
        #region ДОБАВЛЕНИЕ НОВОГО МОДЕМА (ПРОШИВКИ)
        // 1. Добавить модем в GetModemsList
        // 2. Добавить прошивку в GetSoftList
        // 3. Для каких версий прошивок есть конфы IsPresent
        // 4. Возможна ли настройка ТВ MayBeTV
        // 5. Тип передачи конфа в модем GetTansferType
        // 6. Коммада для залития конфа CommonFileComand
        // 7. Добавить инфу в класс ModemInfo, для определения модема (прошивки)
        // 8. Добавить сам конф "rsc_..."
        // 9. В классе ConfCreate:       
          // 9.1 добавить SwitchSoft_...          
          // 9.2 поправить CreateConfiguration
          // 9.3 при необходимости поправить PassToBase64Code
        #endregion


        // список модемов
        public List<string> GetModemsList()
        {
            List<string> rezult = new List<string>();
            rezult.Add("QTech QDSL-1040 rev B1");           // 0
            rezult.Add("D-Link 2540U BRU T1A");             // 1
            rezult.Add("D-Link 2500U NRU D4");              // 2
            rezult.Add("D-Link 2520U BRS D4");              // 3
            rezult.Add("SagemCom F@st 2804 v5");            // 4
            return (rezult);
        }


        // список доступных прошивок для данного модема
        public List<string> GetSoftList(int indx)
        {
            List<string> rezult = new List<string>();
            switch (indx)
            {
                case 0:                     // QTech QDSL-1040 rev B1
                    rezult.Add("2.3");
                    rezult.Add("2.5");
                    break;

                case 1:                     // D-Link 2540U BRU T1A
                    rezult.Add("1.0.27");
                    rezult.Add("1.0.28");
                    rezult.Add("1.0.30");
                    break;

                case 2:                     // D-Link 2500U NRU D4
                    rezult.Add("1.0.47");
                    rezult.Add("1.0.49");
                    break;
                
                case 3:                     // D-Link 2520U BRS D4
                    rezult.Add("1.0.4");
                    break;
                
                default:                    // SagemCom F@st 2804 v5
                    rezult.Add("5.40Testa4N_STK");
                    rezult.Add("5.47Testa4N_STK");
                    break;
            }
            return (rezult);
        }


        // есть ли настройки для такого модема
        public bool IsPresent(int ModemIndex, int SoftIndex)
        {
            bool rezult = false;
            switch (ModemIndex)
            {
                case 0:                     // QTech QDSL-1040 rev B1
                    switch (SoftIndex)
                    {
                        case 0:                     // 2.3
                            rezult = true;
                            break;
                        case 1:                     // 2.5
                            rezult = true;
                            break;
                    }
                    break;

                case 1:                     // D-Link 2540U BRU T1A
                    switch (SoftIndex)
                    {
                        case 0:                     // 1.0.27
                            rezult = false;
                            break;
                        case 1:                     // 1.0.28
                            rezult = true;
                            break;
                        case 2:                     // 1.0.30
                            rezult = true;
                            break;
                    }
                    break;

                case 2:                     // D-Link 2500U NRU D4
                    switch (SoftIndex)
                    {
                        case 0:                     // 1.0.47
                            rezult = false;
                            break;
                        case 1:                     // 1.0.49
                            rezult = true;
                            break;
                    }
                    break;
                
                case 3:                     // D-Link 2520U BRS D4
                    switch (SoftIndex)
                    {
                        case 0:                     // 1.0.4
                            rezult = true;
                            break;
                    }
                    break;

                default:                    // SagemCom F@st 2804 v5
                    rezult = false;                    // 5.40Testa4N_STK
                    break;
            }
            
            return rezult;
        }


        /// <summary>
        /// Возможна ли настройка ТВ для данной прошивки данного модема
        /// </summary>
        /// <param name="ModemIndex">Индекс модема</param>
        /// <param name="SoftIndex">индекс софта модема</param>
        /// <returns></returns>
        public bool MayBeTV(int ModemIndex, int SoftIndex)
        {
            bool rezult = false;
            switch (ModemIndex)
            {
                case 0:                     // QTech QDSL-1040 rev B1
                    switch (SoftIndex)
                    {
                        case 0:                     // 2.3
                            rezult = false;
                            break;
                        case 1:                     // 2.5
                            rezult = true;
                            break;
                    }
                    break;

                case 1:                     // D-Link 2540U BRU T1A
                    switch (SoftIndex)
                    {
                        case 0:                     // 1.0.27
                            rezult = false;
                            break;
                        case 1:                     // 1.0.28
                            rezult = true;
                            break;
                        case 2:                     // 1.0.30
                            rezult = true;
                            break;
                    }
                    break;

                case 2:                     // D-Link 2500U NRU D4
                    switch (SoftIndex)
                    {
                        case 0:                     // 1.0.47
                            rezult = false;
                            break;
                        case 1:                     // 1.0.49
                            rezult = true;
                            break;
                    }
                    break;
                
                case 3:                     // D-Link 2520U BRS D4
                    switch (SoftIndex)
                    {
                        case 0:                     // 1.0.4
                            rezult = true;
                            break;
                    }
                    break;

                default:                    // SagemCom F@st 2804 v5
                    rezult = true;                    // 5.40Testa4N_STK
                    break;
            }
            
            return rezult;
        }


        /// <summary>
        /// Способ передачи модему файла конфигурации
        /// </summary>
        /// <param name="ModemNum"></param>
        /// <param name="SoftNum"></param>
        /// <returns></returns>
        public TransferTyte GetTansferType(int ModemNum, int SoftNum)
        {
            TransferTyte rezult = TransferTyte.none;
            switch (ModemNum)
            {
                case 0:// QTech QDSL-1040 rev B1
                    switch (SoftNum)
                    {
                        case 0:                     // 2.3
                            rezult = TransferTyte.tftp;
                            break;
                        case 1:                     // 2.5
                            rezult = TransferTyte.tftp;
                            break;
                    }
                    break;

                case 1:                     // D-Link 2540U BRU T1A
                    switch (SoftNum)
                    {
                        case 0:                     // 1.0.27
                            rezult = TransferTyte.http;
                            break;
                        case 1:                     // 1.0.28
                            rezult = TransferTyte.http;
                            break;
                        case 2:                     // 1.0.30
                            rezult = TransferTyte.http;
                            break;
                    }
                    break;

                case 2:                     // D-Link 2500U NRU D4
                    rezult = TransferTyte.http;
                    break;
                
                case 3:                     // D-Link 2520U BRS D4
                    rezult = TransferTyte.http;
                    break;
            }

            return rezult;
        }



        /// <summary>
        /// Комманда для заливки конфа
        /// </summary>
        /// <param name="ModemNum"></param>
        /// <param name="SoftNum"></param>
        /// <param name="fName"></param>
        /// <param name="ServerIp"></param>
        /// <returns></returns>
        public string CommonFileComand(int ModemNum, int SoftNum, string fName, string ServerIp)
        {
            //string rezult = string.Empty;
            string rezult = "нет в базе";
            switch (ModemNum)
            {
                case 0:                     // QTech QDSL-1040 rev B1
                    switch (SoftNum)
                    {
                        case 0:                     // 2.3
                            rezult = "tftp -g -t c -f " + fName + " " + ServerIp;
                            break;
                        case 1:                     // 2.5
                            rezult = "tftp -g -t c -f " + fName + " " + ServerIp;
                            break;
                    }
                    break;

                case 1:                     // D-Link 2540U BRU T1A
                    rezult = "restore http://" + ServerIp + "//" + fName;
                    break;

                case 2:                     // D-Link 2500U NRU D3
                    rezult = "restore http://" + ServerIp + "//" + fName;
                    break;

                case 3:                     // D-Link 2520U BRS D3
                    rezult = "restore http://" + ServerIp + "//" + fName;
                    break;
            }

            return rezult;

        }
    }
}
