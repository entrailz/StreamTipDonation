using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace StreamTipDonationUpdater
{
    public partial class Users : Form
    {
        static SQLiteConnection m_dbConnection;
        public Users()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql = "SELECT `amount` FROM `DonationsTest` WHERE `nickname` = '" + tbName.Text + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tbAmount.Text = reader["amount"].ToString();
            }
            buttonBlue1.Enabled = true;
            tbAmount.Enabled = true;
        }

        private void buttonBlue1_Click(object sender, EventArgs e)
        {
            m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql = "UPDATE `DonationsTest` SET `amount` = '" + tbAmount.Text + "' WHERE `nickname` = '" + tbName.Text + "'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            ImRaisingDonationUpdater.Form1.rewriteFile();
        }
    }
}
