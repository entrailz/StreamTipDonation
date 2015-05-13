using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImRaisingDonationUpdater
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();
        IniFile file = new IniFile("settings.ini");
        private string tf = "TEXT_FILES";

        private void Settings_Load(object sender, EventArgs e)
        {
            if (File.Exists("settings.ini"))
            {
                apiKeyTB.Text = file.Read("API_KEY");
                tbAPIToken.Text = file.Read("API_TOKEN");
                textBox1.Text = file.Read("2k", tf);
                textBox2.Text = file.Read("1k", tf);
                txtBox1.Text = file.Read("600", tf);
                txtBox2.Text = file.Read("450", tf);
                textBox3.Text = file.Read("300", tf);
                txtBox3.Text = file.Read("200", tf);
                textBox4.Text = file.Read("100", tf);
                checkBox1.Checked = Convert.ToBoolean(file.Read("DEBUGGING", "DEV"));
            }
            ofd.Filter = "Text Files|*.txt";
        }

        private void buttonBlue1_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void buttonGreen1_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
            }
        }

        private void buttonBlue2_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox4.Text = ofd.FileName;
            }
        }

        private void buttonGreen2_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox3.Text = ofd.FileName;
            }
        }

        private void buttonBlue3_Click(object sender, EventArgs e)
        {
            file.Write("API_KEY", apiKeyTB.Text);
            file.Write("API_TOKEN", tbAPIToken.Text);
            file.Write("2k", textBox1.Text, tf);
            file.Write("1k", textBox2.Text, tf);
            file.Write("600", txtBox1.Text, tf);
            file.Write("450", txtBox2.Text, tf);
            file.Write("300", textBox3.Text, tf);
            file.Write("200", txtBox3.Text, tf);
            file.Write("100", textBox4.Text, tf);
            if (checkBox1.Checked)
            {
                file.Write("DEBUGGING", "true", "DEV");
            }
            else
            {
                file.Write("DEBUGGING", "false", "DEV");
            }
            this.Close();
        }

        private void buttonBlue4_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBox1.Text = ofd.FileName;
            }
        }

        private void buttonGreen3_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBox2.Text = ofd.FileName;
            }
        }

        private void buttonBlue5_Click_1(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox3.Text = ofd.FileName;
            }
        }

        private void buttonGreen4_Click(object sender, EventArgs e)
        {
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtBox3.Text = ofd.FileName;
            }
        }
    }
}
