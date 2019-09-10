using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AMCM
{
    public enum EncapType { PPPoA, PPPoE };
    public enum PortMappingsType { ForAll, SelectedPorts };
    public enum TransferTyte { none, tftp, http };

    public struct AllOtions
    {
        public int ModemNum;
        public int SoftNum;
        public EncapType Encapsulation;
        public string Login;
        public string Password;
        public bool IpTV;
        public PortMappingsType PMap;
    };

    public struct ModemInfoStruct
    {
        public int ModemNum;
        public int SoftNum;
    }

    public partial class MainForm : Form
    {

        private bool IsPresent;
        private bool MayBeTV;
        private AllOtions Settings;// = new AllOtions();
        string ConfFileName;


        // ==========================================================================================
        public MainForm()
        {
            InitializeComponent();
            MayBeTV = true;
            ConfFileName = string.Empty;
        }

        
        // загрузка формы
        private void MainForm_Load(object sender, EventArgs e)
        {
            //btnSend.Visible = false;
            //btnApply.Left += 80;
            
            
            // --- список модемов
            // класс
            ModemList ML = new ModemList();
            // список модемо
            List<string> SL = ML.GetModemsList();
            // очистить бокс
            cmbModem.Items.Clear();
            // заполнить бокс
            for (int i = 0; i < SL.Count; i++)
                cmbModem.Items.Add(SL[i]);
            
            // выбрать 
            //cmbModem.SelectedIndex = 0;

            toolTip1.SetToolTip(btnGetInfo, "Попытаться определить программно");
            toolTip1.SetToolTip(txbFolder, "Папка для сохранения файла конфигурации");
        }


        // выбор модема
        private void cmbModem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.ModemNum = cmbModem.SelectedIndex;
            
            // список прошивок для данной модели
            // класс
            ModemList ML = new ModemList();
            // список прошивок для выбранного модема
            List<string> SL = ML.GetSoftList(cmbModem.SelectedIndex);
            // очистить бокс
            cmbSoft.Items.Clear();
            // заполнить бокс
            for (int i = 0; i < SL.Count; i++)
                cmbSoft.Items.Add(SL[i]);

            // выбрать 
            cmbSoft.SelectedIndex = 0;

            // проверить остальные поля
            AllFieldComplite();
        }


        
        // выбор прошивки
        private void cmbSoft_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.SoftNum = cmbSoft.SelectedIndex;

            ModemList ML = new ModemList();

            // есть ли конфы для такого модема
            IsPresent = ML.IsPresent(cmbModem.SelectedIndex, cmbSoft.SelectedIndex);
            if (IsPresent == false)
                groupBox2.Enabled = false;
            else
            {
                groupBox2.Enabled = true;
                MayBeTV = ML.MayBeTV(cmbModem.SelectedIndex, cmbSoft.SelectedIndex);
                if (MayBeTV == true)
                {
                    if (rbPPPoA.Checked == true)
                    {
                        chbIpTV.Enabled = true;
                        //groupIpTV.Enabled = true;
                    }
                }
                else
                {
                    chbIpTV.Enabled = false;
                    chbIpTV.Checked = false;
                    groupIpTV.Enabled = false;
                }


                // проверить остальные поля
                AllFieldComplite();
            }
        }



        // все ли поля заполнены
        private void AllFieldComplite()
        {
            btnApply.Enabled = false;
            if (cmbModem.SelectedIndex >= 0)    // выбран ли модем
            {
                if (cmbSoft.SelectedIndex >= 0)     // выбрана ли прошивка
                {
                    if (txbLogin.Text.Length > 0)       // введен ли логин
                    {
                        if (txbPassw.Text.Length > 0)       // введен ли пароль
                        {
                            if (txbFolder.Text.Length > 0)       // выбрана ли папка
                                btnApply.Enabled = true;
                        }
                    }
                }
            }
        }


        // изменение текста логина
        private void txbLogin_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            // проверить остальные поля
            AllFieldComplite();
        }


        // изменение текста пароля
        private void txbPassw_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            // проверить остальные поля
            AllFieldComplite();
        }


        // изменение текста пути к папке
        private void txbFolder_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            // проверить остальные поля
            AllFieldComplite();

            // сохранить папку
            Properties.Settings.Default.OutFolder = txbFolder.Text;
            Properties.Settings.Default.Save();
        }


        // смена папки.клик
        private void btnChangeFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txbFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        // выход
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // создать.клик
        private void btnApply_Click(object sender, EventArgs e)
        {
            ConfFileName = string.Empty;

            Settings = new AllOtions();
            
            Settings.ModemNum = cmbModem.SelectedIndex;
            Settings.SoftNum = cmbSoft.SelectedIndex;

            if (rbPPPoA.Checked == true)
                Settings.Encapsulation = EncapType.PPPoA;
            else
                Settings.Encapsulation = EncapType.PPPoE;
            
            Settings.Login = txbLogin.Text;
            Settings.Password = txbPassw.Text;
            
            Settings.IpTV = chbIpTV.Checked;
            
            if (rbTVToAll.Checked == true)
                Settings.PMap = PortMappingsType.ForAll;
            else
                Settings.PMap = PortMappingsType.SelectedPorts;

            // класс
            ConfCreate CC = new ConfCreate(Settings, txbFolder.Text);
            // создать конфигурацию
            CC.CreateConfiguration();

            
            if (CC.ConfFileName.Length > 0)
            {
                ConfFileName = CC.ConfFileName;

                // попробовать влить
                btnSend.Enabled = true;

            }
            
        }

        
        // Активация Чекбокса ТВ
        private void chbIpTV_CheckedChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            groupIpTV.Enabled = chbIpTV.Checked;
        }


        // Выбор РРРоЕ
        private void rbPPPoE_CheckedChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            if (rbPPPoE.Checked == true)
            {
                chbIpTV.Checked = false;
                chbIpTV.Enabled = false;
            }
        }

        
        // Выбор РРРоА
        private void rbPPPoA_CheckedChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            if (rbPPPoA.Checked == true)
                if (MayBeTV == true)
                    chbIpTV.Enabled = true;
        }



        // Определить модем
        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            ModemInfo MI = new ModemInfo();
            ModemInfoStruct MIS = MI.GetModemInfo();

            this.Enabled = true;

            cmbModem.SelectedIndex = MIS.ModemNum;
            //cmbModem_SelectedIndexChanged(cmbModem, new EventArgs());
            cmbSoft.SelectedIndex = MIS.SoftNum;

        }

        
        // отправить
        private void btnSend_Click(object sender, EventArgs e)
        {
            

            SendConfigForm SCF = new SendConfigForm();
            //SCF.Modem = cmbModem.Text;
            SCF.ModemInd = cmbModem.SelectedIndex;
            //SCF.SoftVer = "ver: " + cmbSoft.Text;
            SCF.SoftVerInd = cmbSoft.SelectedIndex;
            //SCF.ConfFile = ConfFileName;
            SCF.ConfFile = System.IO.Path.GetFileName(ConfFileName);
            SCF.WorkDir = System.IO.Path.GetDirectoryName(ConfFileName);

            this.Hide();
            SCF.ShowDialog();
            this.Show();

        }

        private void rbTVToAll_CheckedChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
        }

        private void rbTVSomePorts_CheckedChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
        }


        // About
        private void label5_Click(object sender, EventArgs e)
        {
            AboutForm AFr = new AboutForm();
            AFr.ShowDialog();
        }


        
        
    }
}
