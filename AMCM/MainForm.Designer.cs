namespace AMCM
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetInfo = new System.Windows.Forms.Button();
            this.cmbSoft = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbModem = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbIpTV = new System.Windows.Forms.CheckBox();
            this.groupIpTV = new System.Windows.Forms.GroupBox();
            this.rbTVSomePorts = new System.Windows.Forms.RadioButton();
            this.rbTVToAll = new System.Windows.Forms.RadioButton();
            this.txbPassw = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txbLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbPPPoE = new System.Windows.Forms.RadioButton();
            this.rbPPPoA = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnChangeFolder = new System.Windows.Forms.Button();
            this.txbFolder = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSend = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupIpTV.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetInfo);
            this.groupBox1.Controls.Add(this.cmbSoft);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbModem);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 82);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Модем";
            // 
            // btnGetInfo
            // 
            this.btnGetInfo.Location = new System.Drawing.Point(222, 48);
            this.btnGetInfo.Name = "btnGetInfo";
            this.btnGetInfo.Size = new System.Drawing.Size(79, 23);
            this.btnGetInfo.TabIndex = 2;
            this.btnGetInfo.Text = "Определить";
            this.btnGetInfo.UseVisualStyleBackColor = true;
            this.btnGetInfo.Click += new System.EventHandler(this.btnGetInfo_Click);
            // 
            // cmbSoft
            // 
            this.cmbSoft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSoft.FormattingEnabled = true;
            this.cmbSoft.Location = new System.Drawing.Point(78, 50);
            this.cmbSoft.Name = "cmbSoft";
            this.cmbSoft.Size = new System.Drawing.Size(121, 21);
            this.cmbSoft.TabIndex = 1;
            this.cmbSoft.SelectedIndexChanged += new System.EventHandler(this.cmbSoft_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Версия ПО:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Марка:";
            // 
            // cmbModem
            // 
            this.cmbModem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModem.FormattingEnabled = true;
            this.cmbModem.Location = new System.Drawing.Point(53, 17);
            this.cmbModem.Name = "cmbModem";
            this.cmbModem.Size = new System.Drawing.Size(248, 21);
            this.cmbModem.TabIndex = 0;
            this.cmbModem.SelectedIndexChanged += new System.EventHandler(this.cmbModem_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chbIpTV);
            this.groupBox2.Controls.Add(this.groupIpTV);
            this.groupBox2.Controls.Add(this.txbPassw);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txbLogin);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.rbPPPoE);
            this.groupBox2.Controls.Add(this.rbPPPoA);
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 105);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройки";
            // 
            // chbIpTV
            // 
            this.chbIpTV.AutoSize = true;
            this.chbIpTV.Location = new System.Drawing.Point(212, 20);
            this.chbIpTV.Name = "chbIpTV";
            this.chbIpTV.Size = new System.Drawing.Size(52, 17);
            this.chbIpTV.TabIndex = 4;
            this.chbIpTV.Text = "Ip-TV";
            this.chbIpTV.UseVisualStyleBackColor = true;
            this.chbIpTV.CheckedChanged += new System.EventHandler(this.chbIpTV_CheckedChanged);
            // 
            // groupIpTV
            // 
            this.groupIpTV.Controls.Add(this.rbTVSomePorts);
            this.groupIpTV.Controls.Add(this.rbTVToAll);
            this.groupIpTV.Enabled = false;
            this.groupIpTV.Location = new System.Drawing.Point(203, 22);
            this.groupIpTV.Name = "groupIpTV";
            this.groupIpTV.Size = new System.Drawing.Size(91, 75);
            this.groupIpTV.TabIndex = 5;
            this.groupIpTV.TabStop = false;
            // 
            // rbTVSomePorts
            // 
            this.rbTVSomePorts.AutoSize = true;
            this.rbTVSomePorts.Location = new System.Drawing.Point(9, 45);
            this.rbTVSomePorts.Name = "rbTVSomePorts";
            this.rbTVSomePorts.Size = new System.Drawing.Size(68, 17);
            this.rbTVSomePorts.TabIndex = 1;
            this.rbTVSomePorts.Text = "1-2 Порт";
            this.rbTVSomePorts.UseVisualStyleBackColor = true;
            this.rbTVSomePorts.CheckedChanged += new System.EventHandler(this.rbTVSomePorts_CheckedChanged);
            // 
            // rbTVToAll
            // 
            this.rbTVToAll.AutoSize = true;
            this.rbTVToAll.Checked = true;
            this.rbTVToAll.Location = new System.Drawing.Point(9, 22);
            this.rbTVToAll.Name = "rbTVToAll";
            this.rbTVToAll.Size = new System.Drawing.Size(72, 17);
            this.rbTVToAll.TabIndex = 0;
            this.rbTVToAll.TabStop = true;
            this.rbTVToAll.Text = "Для всех";
            this.rbTVToAll.UseVisualStyleBackColor = true;
            this.rbTVToAll.CheckedChanged += new System.EventHandler(this.rbTVToAll_CheckedChanged);
            // 
            // txbPassw
            // 
            this.txbPassw.Location = new System.Drawing.Point(53, 72);
            this.txbPassw.Name = "txbPassw";
            this.txbPassw.Size = new System.Drawing.Size(132, 20);
            this.txbPassw.TabIndex = 3;
            this.txbPassw.TextChanged += new System.EventHandler(this.txbPassw_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Пароль:";
            // 
            // txbLogin
            // 
            this.txbLogin.Location = new System.Drawing.Point(53, 46);
            this.txbLogin.Name = "txbLogin";
            this.txbLogin.Size = new System.Drawing.Size(132, 20);
            this.txbLogin.TabIndex = 2;
            this.txbLogin.TextChanged += new System.EventHandler(this.txbLogin_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Логин:";
            // 
            // rbPPPoE
            // 
            this.rbPPPoE.AutoSize = true;
            this.rbPPPoE.Location = new System.Drawing.Point(88, 19);
            this.rbPPPoE.Name = "rbPPPoE";
            this.rbPPPoE.Size = new System.Drawing.Size(59, 17);
            this.rbPPPoE.TabIndex = 1;
            this.rbPPPoE.Text = "PPPoE";
            this.rbPPPoE.UseVisualStyleBackColor = true;
            this.rbPPPoE.CheckedChanged += new System.EventHandler(this.rbPPPoE_CheckedChanged);
            // 
            // rbPPPoA
            // 
            this.rbPPPoA.AutoSize = true;
            this.rbPPPoA.Checked = true;
            this.rbPPPoA.Location = new System.Drawing.Point(9, 19);
            this.rbPPPoA.Name = "rbPPPoA";
            this.rbPPPoA.Size = new System.Drawing.Size(59, 17);
            this.rbPPPoA.TabIndex = 0;
            this.rbPPPoA.TabStop = true;
            this.rbPPPoA.Text = "PPPoA";
            this.rbPPPoA.UseVisualStyleBackColor = true;
            this.rbPPPoA.CheckedChanged += new System.EventHandler(this.rbPPPoA_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnChangeFolder);
            this.groupBox3.Controls.Add(this.txbFolder);
            this.groupBox3.Location = new System.Drawing.Point(12, 211);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(310, 53);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Папка";
            // 
            // btnChangeFolder
            // 
            this.btnChangeFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeFolder.Image")));
            this.btnChangeFolder.Location = new System.Drawing.Point(276, 17);
            this.btnChangeFolder.Name = "btnChangeFolder";
            this.btnChangeFolder.Size = new System.Drawing.Size(25, 23);
            this.btnChangeFolder.TabIndex = 1;
            this.btnChangeFolder.UseVisualStyleBackColor = true;
            this.btnChangeFolder.Click += new System.EventHandler(this.btnChangeFolder_Click);
            // 
            // txbFolder
            // 
            this.txbFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AMCM.Properties.Settings.Default, "OutFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txbFolder.Location = new System.Drawing.Point(9, 19);
            this.txbFolder.Name = "txbFolder";
            this.txbFolder.Size = new System.Drawing.Size(261, 20);
            this.txbFolder.TabIndex = 0;
            this.txbFolder.Text = global::AMCM.Properties.Settings.Default.OutFolder;
            this.txbFolder.TextChanged += new System.EventHandler(this.txbFolder_TextChanged);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(12, 270);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(158, 28);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Создать файл кофигурации";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(257, 270);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(65, 28);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSend
            // 
            this.btnSend.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(176, 270);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 28);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(293, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "About";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(331, 304);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADSL Modem Config\'s Maker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupIpTV.ResumeLayout(false);
            this.groupIpTV.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSoft;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbModem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txbPassw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbPPPoE;
        private System.Windows.Forms.RadioButton rbPPPoA;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnChangeFolder;
        private System.Windows.Forms.TextBox txbFolder;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnGetInfo;
        private System.Windows.Forms.CheckBox chbIpTV;
        private System.Windows.Forms.GroupBox groupIpTV;
        private System.Windows.Forms.RadioButton rbTVSomePorts;
        private System.Windows.Forms.RadioButton rbTVToAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label5;
    }
}

