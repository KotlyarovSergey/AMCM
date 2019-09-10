namespace AMCM
{
    partial class SendConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendConfigForm));
            this.lblModem = new System.Windows.Forms.Label();
            this.lblSoft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.txbServIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbCommand = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnReboot = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblModem
            // 
            this.lblModem.AutoSize = true;
            this.lblModem.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblModem.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblModem.Location = new System.Drawing.Point(3, 6);
            this.lblModem.Name = "lblModem";
            this.lblModem.Size = new System.Drawing.Size(72, 22);
            this.lblModem.TabIndex = 0;
            this.lblModem.Text = "Modem:";
            // 
            // lblSoft
            // 
            this.lblSoft.AutoSize = true;
            this.lblSoft.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSoft.Location = new System.Drawing.Point(3, 28);
            this.lblSoft.Name = "lblSoft";
            this.lblSoft.Size = new System.Drawing.Size(41, 22);
            this.lblSoft.TabIndex = 1;
            this.lblSoft.Text = "Soft:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Протокол:";
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Items.AddRange(new object[] {
            "TFTP",
            "HTTP"});
            this.cmbProtocol.Location = new System.Drawing.Point(129, 11);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(77, 21);
            this.cmbProtocol.TabIndex = 3;
            this.cmbProtocol.SelectedIndexChanged += new System.EventHandler(this.cmbProtocol_SelectedIndexChanged);
            // 
            // txbServIp
            // 
            this.txbServIp.Enabled = false;
            this.txbServIp.Font = new System.Drawing.Font("Eras Demi ITC", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbServIp.Location = new System.Drawing.Point(4, 63);
            this.txbServIp.Name = "txbServIp";
            this.txbServIp.Size = new System.Drawing.Size(126, 26);
            this.txbServIp.TabIndex = 5;
            this.txbServIp.Text = "0.0.0.0";
            this.txbServIp.TextChanged += new System.EventHandler(this.txbServIp_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "IP-адресс сервера:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Комманда:";
            // 
            // txbCommand
            // 
            this.txbCommand.Enabled = false;
            this.txbCommand.Location = new System.Drawing.Point(4, 119);
            this.txbCommand.Name = "txbCommand";
            this.txbCommand.Size = new System.Drawing.Size(202, 20);
            this.txbCommand.TabIndex = 8;
            this.txbCommand.TextChanged += new System.EventHandler(this.txbCommand_TextChanged);
            // 
            // btnRun
            // 
            this.btnRun.Enabled = false;
            this.btnRun.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRun.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnRun.Location = new System.Drawing.Point(4, 235);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(214, 32);
            this.btnRun.TabIndex = 9;
            this.btnRun.Text = "Обновить конфигурацию";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnReboot
            // 
            this.btnReboot.Enabled = false;
            this.btnReboot.Location = new System.Drawing.Point(5, 273);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(142, 27);
            this.btnReboot.TabIndex = 10;
            this.btnReboot.Text = "Перезагрузить модем";
            this.btnReboot.UseVisualStyleBackColor = true;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblModem);
            this.panel1.Controls.Add(this.lblSoft);
            this.panel1.Location = new System.Drawing.Point(5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(213, 59);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txbServIp);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cmbProtocol);
            this.panel2.Controls.Add(this.txbCommand);
            this.panel2.Location = new System.Drawing.Point(5, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 149);
            this.panel2.TabIndex = 12;
            // 
            // SendConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 307);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnReboot);
            this.Controls.Add(this.btnRun);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SendConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SendConfigForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.SendConfigForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblModem;
        private System.Windows.Forms.Label lblSoft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.TextBox txbServIp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbCommand;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}