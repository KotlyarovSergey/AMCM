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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            // Версия
            string AppVers = Application.ProductVersion;
            label2.Text = "Version: " + AppVers;
            // дата

            // модемы
            string M = string.Empty;
            ModemList ML = new ModemList();
            List<string> Modems = ML.GetModemsList();
            for (int i = 0; i < Modems.Count; i++)
            {
                //if (M.Length > 0)
                  //  M += "\r\n";
                M += (i + 1).ToString() + ". " + Modems[i] + "\r\n";
                List<string> Softs = ML.GetSoftList(i);
                for (int k = 0; k < Softs.Count; k++)
                {
                    M += "    ver.: " + Softs[k] + "\r\n";
                }
            }
            
            textBox1.Text = M;
            //textBox1.SelectionStart = 0;
            textBox1.ScrollBars = ScrollBars.Vertical;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
