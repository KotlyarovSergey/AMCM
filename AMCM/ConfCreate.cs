using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace AMCM
{
    class ConfCreate
    {
        public string ConfFileName;

        private AllOtions Settings;
        private string FolderPath;


        public ConfCreate(AllOtions ModemSettings, string Path)
        {
            Settings = ModemSettings;
            FolderPath = Path;
            ConfFileName = string.Empty;
        }


        public void CreateConfiguration()
        {
            switch (Settings.ModemNum)
            {
                case 0:                 // QTech QDSL-1040 rev B1
                    SwitchSoft_Q1040_B1();
                    break;

                case 1:                 // D-Link 2540U BRU T1A
                    SwitchSoft_D2540_T1A();
                    break;
                case 2:                 // D-Link 2500U NRU D4
                    SwitchSoft_D2500_D4();
                    break;
                case 3:                 // D-Link 2520U BRS D4
                    SwitchSoft_D2520_D4();
                    break;
            }


        }


        // QTech QDSL-1040 rev B1
        private void SwitchSoft_Q1040_B1()
        {
            switch (Settings.SoftNum)
            {

                case 0:                 // 2.3
                    if (Settings.Encapsulation == EncapType.PPPoE)
                    {
                        MakeFile(rsc_Q1040_B1_2_3.PPPoE_1, rsc_Q1040_B1_2_3.PPPoE_2, rsc_Q1040_B1_2_3.PPPoE_3);
                    }
                    else    // PPPoA
                    {
                        if (Settings.IpTV == false)
                        {
                            MakeFile(rsc_Q1040_B1_2_3.PPPoA_1, rsc_Q1040_B1_2_3.PPPoA_2, rsc_Q1040_B1_2_3.PPPoA_3);
                        }
                    }
                    break;

                case 1:                 // 2.5
                    if (Settings.Encapsulation == EncapType.PPPoE)
                    {
                        MakeFile(rsc_Q1040_B1_2_5.PPPoE_1, rsc_Q1040_B1_2_5.PPPoE_2, rsc_Q1040_B1_2_5.PPPoE_3);
                    }
                    else    // PPPoA
                    {
                        if (Settings.IpTV == false)
                        {
                            MakeFile(rsc_Q1040_B1_2_5.PPPoA_1, rsc_Q1040_B1_2_5.PPPoA_2, rsc_Q1040_B1_2_5.PPPoA_3);
                        }
                        else    // IP-TV
                        {
                            if (Settings.PMap == PortMappingsType.ForAll)
                                MakeFile(rsc_Q1040_B1_2_5.PPPoA_TV_1, rsc_Q1040_B1_2_5.PPPoA_TV_2, rsc_Q1040_B1_2_5.PPPoA_TV_3);
                            else
                                MakeFile(rsc_Q1040_B1_2_5.PPPoA_TV_Group_1, rsc_Q1040_B1_2_5.PPPoA_TV_Group_2, rsc_Q1040_B1_2_5.PPPoA_TV_Group_3);
                        }
                    }

                    break;
            }
        }


        // D-Link 2540U BRU T1A
        private void SwitchSoft_D2540_T1A()
        {
            switch (Settings.SoftNum)
            {
                case 0:             // 1.0.27

                    break;

                case 1:             // 1.0.28
                    if (Settings.Encapsulation == EncapType.PPPoE)
                    {
                        MakeFile(rsc_D2540_T1A_1_0_28_30.PPPoE_1, rsc_D2540_T1A_1_0_28_30.PPPoE_2, rsc_D2540_T1A_1_0_28_30.PPPoE_3);
                    }
                    else    // PPPoA
                    {
                        if (Settings.IpTV == false)
                        {
                            MakeFile(rsc_D2540_T1A_1_0_28_30.PPPoA_1, rsc_D2540_T1A_1_0_28_30.PPPoA_2, rsc_D2540_T1A_1_0_28_30.PPPoA_3);
                        }
                        else    // IP-TV
                        {
                            if (Settings.PMap == PortMappingsType.ForAll)
                                MakeFile(rsc_D2540_T1A_1_0_28_30.PPPoA_TV_1, rsc_D2540_T1A_1_0_28_30.PPPoA_TV_2, rsc_D2540_T1A_1_0_28_30.PPPoA_TV_3);
                            else
                                MakeFile(rsc_D2540_T1A_1_0_28_30.PPPoA_TV_Group_1, rsc_D2540_T1A_1_0_28_30.PPPoA_TV_Group_2, rsc_D2540_T1A_1_0_28_30.PPPoA_TV_Group_3);
                        }
                    }
                    break;

                case 2:             // 1.0.30
                    if (Settings.Encapsulation == EncapType.PPPoE)
                    {
                        MakeFile(rsc_D2540_T1A_1_0_28_30.PPPoE_1, rsc_D2540_T1A_1_0_28_30.PPPoE_2, rsc_D2540_T1A_1_0_28_30.PPPoE_3);
                    }
                    else    // PPPoA
                    {
                        if (Settings.IpTV == false)
                        {
                            MakeFile(rsc_D2540_T1A_1_0_28_30.PPPoA_1, rsc_D2540_T1A_1_0_28_30.PPPoA_2, rsc_D2540_T1A_1_0_28_30.PPPoA_3);
                        }
                        else    // IP-TV
                        {
                            if (Settings.PMap == PortMappingsType.ForAll)
                                MakeFile(rsc_D2540_T1A_1_0_28_30.PPPoA_TV_1, rsc_D2540_T1A_1_0_28_30.PPPoA_TV_2, rsc_D2540_T1A_1_0_28_30.PPPoA_TV_3);
                            else
                                MakeFile(rsc_D2540_T1A_1_0_28_30.PPPoA_TV_Group_1, rsc_D2540_T1A_1_0_28_30.PPPoA_TV_Group_2, rsc_D2540_T1A_1_0_28_30.PPPoA_TV_Group_3);
                        }
                    }
                    break;
            }
        }


        // D-Link 2500U NRU D4
        private void SwitchSoft_D2500_D4()
        {
            switch (Settings.SoftNum)
            {
                case 0:             // 1.0.47

                    break;

                case 1:             // 1.0.49
                    if (Settings.Encapsulation == EncapType.PPPoE)
                    {
                        MakeFile(rsc_D2500_D4_1_0_49.PPPoE_1, rsc_D2500_D4_1_0_49.PPPoE_2, rsc_D2500_D4_1_0_49.PPPoE_3);
                    }
                    else    // PPPoA
                    {
                        if (Settings.IpTV == false)
                        {
                            MakeFile(rsc_D2500_D4_1_0_49.PPPoA_1, rsc_D2500_D4_1_0_49.PPPoA_2, rsc_D2500_D4_1_0_49.PPPoA_3);
                        }
                        else    // IP-TV
                        {
                            MakeFile(rsc_D2500_D4_1_0_49.PPPoA_TV_1, rsc_D2500_D4_1_0_49.PPPoA_TV_2, rsc_D2500_D4_1_0_49.PPPoA_TV_3);
                        }
                    }
                    break;
            }
        }


        // D-Link 2520U BRS D4
        private void SwitchSoft_D2520_D4()
        {
            switch (Settings.SoftNum)
            {
                case 0:             // 1.0.4
                    if (Settings.Encapsulation == EncapType.PPPoE)
                    {
                        MakeFile(rsc_D2520_D4_1_0_4.PPPoE_1, rsc_D2520_D4_1_0_4.PPPoE_2, rsc_D2520_D4_1_0_4.PPPoE_3);
                    }
                    else    // PPPoA
                    {
                        if (Settings.IpTV == false)
                        {
                            MakeFile(rsc_D2520_D4_1_0_4.PPPoA_1, rsc_D2520_D4_1_0_4.PPPoA_2, rsc_D2520_D4_1_0_4.PPPoA_3);
                        }
                        else    // IP-TV
                        {
                            MakeFile(rsc_D2520_D4_1_0_4.PPPoA_TV_1, rsc_D2520_D4_1_0_4.PPPoA_TV_2, rsc_D2520_D4_1_0_4.PPPoA_TV_3);
                        }
                    }
                    break;
            }
        }


        // ================  PRIVATE =======================
        // Записать файл
        private void MakeFile(string S1, string S2, string S3)
        {
            // S1 + login + S2 + PassB64 + S3
            string fl = GetFileName();

            string PassB64 = PassToBase64Code();

            FileStream FS;
            try
            {
                FS = new FileStream(fl, FileMode.Create, FileAccess.Write);


                byte[] buffer = Encoding.ASCII.GetBytes(S1);
                FS.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes(Settings.Login);
                FS.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes(S2);
                FS.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes(PassB64);
                FS.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes(S3);
                FS.Write(buffer, 0, buffer.Length);

                FS.WriteByte(0);

                FS.Close();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Невозоможно создать файл!", "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                ConfFileName = string.Empty;
                return;
            }

            string s = System.IO.Path.GetFileName(fl);

            //System.Windows.Forms.MessageBox.Show("OK");
            System.Windows.Forms.MessageBox.Show("Файл: \"...\\" + s + "\" успешно создан", "AMCM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
            ConfFileName = fl;
        }



        // выбрать имя для файла
        private string GetFileName()
        {
            string ext = ".conf";
            if (Settings.ModemNum == 1)     // D-Link 2540U BRU T1A
                ext = ".xml";

            string fl = FolderPath + "\\" + Settings.Login + ext;
            if (File.Exists(fl) == false)
                return fl;

            string start = FolderPath + "\\" + Settings.Login + "_";
            

            for (int i = 1; i < int.MaxValue; i++)
            {
                fl = start + i.ToString() + ext;
                if (File.Exists(fl) == false)
                    return fl;
            }

            return fl;
        }



        // конвертировать пароль в Base64
        private string PassToBase64Code()
        {
            //byte[] buf = Encoding.ASCII.GetBytes(Settings.Password);
            //string s = Convert.ToBase64String(buf);
            
            string rezult;

            string s = Convert.ToBase64String(Encoding.ASCII.GetBytes(Settings.Password));
            rezult = s;

            if (Settings.ModemNum == 0 && Settings.SoftNum == 1)    // Qtech 1040 rev. B1 2.5
                rezult = CorrectBase64String(s, Settings.Password.Length);
            else if (Settings.ModemNum == 1)                        // D-Link 2540 T1A
                rezult = CorrectBase64String(s, Settings.Password.Length);
            else if (Settings.ModemNum == 2)                        // D-Link 2500 D4
                rezult = CorrectBase64String(s, Settings.Password.Length);
            else if (Settings.ModemNum == 3)                        // D-Link 2520 D4
                rezult = CorrectBase64String(s, Settings.Password.Length);

            return rezult;
        }


        // ПОДПРАВИТЬ BASE64 КОД
        /// <summary>
        /// Подкорректировать
        /// </summary>
        /// <param name="input">Нормальный Base64</param>
        /// <param name="ln">длинна Пароля (не кода!)</param>
        /// <returns></returns>
        private string CorrectBase64String(string input, int ln)
        {
            //int ln = input.Length;
            int del = ln / 3;
            int razn = ln - del * 3;

            string outstring;
            int len = input.Length;
            switch (razn)
            {
                case 0:                 // добавить к концу
                    outstring = input + "AA==";
                    break;

                case 1:                 // заменить предпоследнюю букву
                    outstring = input.Substring(0, len - 2) + "A" + input.Substring(len-1);
                    break;

                case 2:                 // заменить последнюю букву
                    outstring = input.Substring(0, len - 1) + "A";
                    break;

                default:
                    outstring = input;
                    break;
            }
            return (outstring);
        }
    }
}
