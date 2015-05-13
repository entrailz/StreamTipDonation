using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.IO;
using System.Drawing;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SQLite;
using System.Windows.Forms;
using Quobject.SocketIoClientDotNet.Client;

namespace ImRaisingDonationUpdater
{
    public partial class Form1 : Form
    {

        public static List<string> Queue = new List<string>();
        public static List<string> blackList = new List<string>();
        static SQLiteConnection m_dbConnection;
        IniFile MyIni = new IniFile("settings.ini");

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            log("Attempting to open database...");
            m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
            m_dbConnection.Open();
            log("Database successfully opened.");
            log("Attempting to open stream!");
            //Thread t = new Thread(() => OpenSSEStream("https://imraising.tv/api/v1/listen?apikey=" + MyIni.Read("API_KEY")));
            Thread t = new Thread(() => startMonitoring(MyIni.Read("API_KEY"), MyIni.Read("API_TOKEN")));
            t.IsBackground = true;
            t.Start();
            log("If you see 'authenticated' below, you are set to go!");
            button1.Enabled = false;
        }

        public void startMonitoring(string api, string apitoken)
        {
            IO.Options options = new IO.Options();
            options.QueryString = string.Format("client_id={0}&access_token={1}", api, apitoken);
            var socket = IO.Socket("https://streamtip.com", options);
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                socket.On("authenticated", (data) =>
                {
                    log(data.ToString());
                });
                socket.On("error", (data) =>
                {
                    log(data.ToString());
                });
                socket.On("newTip", (data) =>
                {
                    //log(data.ToString());
                    addDonation(data);
                });
            });
            socket.On(Socket.EVENT_ERROR, (data) =>
            {
                if (data.ToString().Contains("401"))
                {
                    log("Failed auth - incorrect API details.");
                }
                socket.Disconnect();
            });
        }

        public void addDonation(dynamic data)
        {
            string name2 = data.username;
            blackList.Clear();
            string sqlcommand = "SELECT * FROM Blacklist;";
            SQLiteCommand command = new SQLiteCommand(sqlcommand, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                blackList.Add(reader["nickname"].ToString());
            }
            if (blackList.Contains(name2))
            {
                log("Blacklisted name found - not adding donation.");
                return;
            }
            decimal amount = 0;
            string name = "";
            log("New Donation Received!");
            sqlcommand = "INSERT OR REPLACE INTO DonationsTest (nickname, amount) VALUES (COALESCE((SELECT nickname FROM DonationsTest WHERE nickname = '" + data.username + "'), '" + data.username + "'), COALESCE(((SELECT amount FROM DonationsTest WHERE nickname = '" + data.username + "') + " + data.amount + "), '" + data.amount + "'));";
            sqlcommand = sqlcommand.Replace("\"", String.Empty);
            command = new SQLiteCommand(sqlcommand, m_dbConnection);
            command.ExecuteNonQuery();
            sqlcommand = "SELECT * FROM DonationsTest WHERE nickname = '" + data.username + "';";
            sqlcommand = sqlcommand.Replace("\"", String.Empty);
            command = new SQLiteCommand(sqlcommand, m_dbConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                name = reader["nickname"].ToString();
                string tempamount = reader["amount"].ToString();
                amount = amount + Convert.ToDecimal(tempamount);
            }
            log("Total amount donated by: " + name + " is: " + amount.ToString());
            rewriteFile();

        }

        public static void rewriteFile()
        {
            var MyIni = new IniFile("settings.ini");
            if (File.Exists(MyIni.Read("2k", "TEXT_FILES")))
            {
                try
                {
                    File.WriteAllText(MyIni.Read("2k", "TEXT_FILES"), "");
                    File.WriteAllText(MyIni.Read("1k", "TEXT_FILES"), "");
                    File.WriteAllText(MyIni.Read("600", "TEXT_FILES"), "");
                    File.WriteAllText(MyIni.Read("450", "TEXT_FILES"), "");
                    File.WriteAllText(MyIni.Read("300", "TEXT_FILES"), "");
                    File.WriteAllText(MyIni.Read("200", "TEXT_FILES"), "");
                    File.WriteAllText(MyIni.Read("100", "TEXT_FILES"), "");
                    string sqlcommand = "SELECT * FROM DonationsTest ORDER BY amount DESC;";
                    sqlcommand = sqlcommand.Replace("\"", String.Empty);
                    SQLiteCommand command = new SQLiteCommand(sqlcommand, m_dbConnection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int amount = Convert.ToInt32(reader["amount"]);
                        if (amount >= 2000)
                        {
                            File.AppendAllText(MyIni.Read("2k", "TEXT_FILES"), reader["nickname"].ToString() + " $" + amount.ToString() + Environment.NewLine);
                        }
                        else if (amount >= 1000)
                        {
                            File.AppendAllText(MyIni.Read("1k", "TEXT_FILES"), reader["nickname"].ToString() + " $" + amount.ToString() + Environment.NewLine);
                        }
                        else if (amount >= 600)
                        {
                            File.AppendAllText(MyIni.Read("600", "TEXT_FILES"), reader["nickname"].ToString() + " $" + amount.ToString() + Environment.NewLine);
                        }
                        else if (amount >= 450)
                        {
                            File.AppendAllText(MyIni.Read("450", "TEXT_FILES"), reader["nickname"].ToString() + " $" + amount.ToString() + Environment.NewLine);
                        }
                        else if (amount >= 300)
                        {
                            File.AppendAllText(MyIni.Read("300", "TEXT_FILES"), reader["nickname"].ToString() + " $" + amount.ToString() + Environment.NewLine);
                        }
                        else if (amount >= 200)
                        {
                            File.AppendAllText(MyIni.Read("200", "TEXT_FILES"), reader["nickname"].ToString() + " $" + amount.ToString() + Environment.NewLine);
                        }
                        else if (amount >= 100)
                        {
                            File.AppendAllText(MyIni.Read("100", "TEXT_FILES"), reader["nickname"].ToString() + " $" + amount.ToString() + Environment.NewLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("ERROR: Please ensure that the text files are set in options correctly.", "Unable to update text files", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void log(string data)
        {
            if (textBox1.Lines.Count() >= 200)
            {
                if (textBox1.InvokeRequired)
                {
                    textBox1.Invoke(new MethodInvoker(delegate()
                    {
                        textBox1.ResetText();
                    }));
                }
                else
                {
                    textBox1.ResetText();
                }
            }
            if (textBox1.InvokeRequired)
            {
                textBox1.Invoke(new MethodInvoker(delegate()
                    {
                        textBox1.AppendText(data + Environment.NewLine);
                    }));
            }
            else
            {
                textBox1.AppendText(data + Environment.NewLine);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void resetDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset the database, this cannot be reversed.", "Reset Database?", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                File.Copy("Donators.sqlite", "Donators_backup.sqlite", true);
                if (m_dbConnection == null)
                {
                    m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
                    m_dbConnection.Open();
                }
                string sqlcommand = "DELETE FROM DonationsTest;";
                sqlcommand = sqlcommand.Replace("\"", String.Empty);
                SQLiteCommand command = new SQLiteCommand(sqlcommand, m_dbConnection);
                command.ExecuteNonQuery();
                log("All data in database removed.");
                rewriteFile();
            }
        }

        private void importFromtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] data = new string[] { };
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                data = File.ReadAllLines(ofd.FileName);
            }
            if (m_dbConnection == null)
            {
                m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
                m_dbConnection.Open();
            }
            foreach (string s in data)
            {
                try
                {
                    string[] parsed = s.Split(new string[] { " $" }, StringSplitOptions.None);
                    log("Username: " + parsed[0] + " amount: " + parsed[1]);
                    string sqlcommand = "INSERT OR REPLACE INTO DonationsTest (nickname, amount) VALUES (COALESCE((SELECT nickname FROM DonationsTest WHERE nickname = '" + parsed[0] + "'), '" + parsed[0] + "'), COALESCE(((SELECT amount FROM DonationsTest WHERE nickname = '" + parsed[0] + "') + " + parsed[1] + "), '" + parsed[1] + "'));";
                    sqlcommand = sqlcommand.Replace("\"", String.Empty);
                    SQLiteCommand command = new SQLiteCommand(sqlcommand, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            rewriteFile();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showApplication();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showApplication();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showApplication()
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            textBox1.ScrollToCaret();
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                if (m_dbConnection == null)
                {
                    m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
                    m_dbConnection.Open();
                }
                string sqlcommand = "DELETE FROM DonationsTest WHERE nickname='" + toolStripTextBox1.Text + "';";
                sqlcommand = sqlcommand.Replace("\"", String.Empty);
                SQLiteCommand command = new SQLiteCommand(sqlcommand, m_dbConnection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log(ex.Message);
                }
                log("Name: " + toolStripTextBox1.Text + " has been removed from the database.");
                toolStripTextBox1.Text = "";
                rewriteFile();
            }
        }

        private void toolStripTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
                if (m_dbConnection == null)
                {
                    m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
                    m_dbConnection.Open();
                }
                string sqlcommand = "INSERT OR REPLACE INTO Blacklist (nickname) VALUES (COALESCE((SELECT nickname FROM Blacklist WHERE nickname = '" + toolStripTextBox2.Text + "'), '" + toolStripTextBox2.Text + "'));";
                sqlcommand = sqlcommand.Replace("\"", String.Empty);
                SQLiteCommand command = new SQLiteCommand(sqlcommand, m_dbConnection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log(ex.Message);
                }
                log("Name: " + toolStripTextBox2.Text + " will not have their donations updated.");
                toolStripTextBox2.Text = "";
            }
        }

        private void importFromcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Files (*.csv)|*.csv";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                statusStrip1.Visible = true;
                Thread t = new Thread(() => insertFromCSV(ofd.FileName));
                t.IsBackground = true;
                t.Start();
            }
        }

        private void insertFromCSV(string path)
        {
            int amountchecked = 0;
            string[] data = File.ReadAllLines(path);
            if (m_dbConnection == null)
            {
                m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
                m_dbConnection.Open();
            }
            foreach (string s in data)
            {
                string a = s.Replace("\"", "");
                if (s.Contains("Nickname"))
                {

                }
                else
                {
                    string[] split = a.Split(',');
                    log(String.Format("Name: {0} Amount: {1}", split[0], split[3]));
                    string sqlcommand = "INSERT OR REPLACE INTO DonationsTest (nickname, amount) VALUES (COALESCE((SELECT nickname FROM DonationsTest WHERE nickname = '" + split[0] + "'), '" + split[0] + "'), COALESCE(((SELECT amount FROM DonationsTest WHERE nickname = '" + split[0] + "') + " + split[3] + "), '" + split[3] + "'));";
                    sqlcommand = sqlcommand.Replace("\"", String.Empty);
                    SQLiteCommand command = new SQLiteCommand(sqlcommand, m_dbConnection);
                    command.ExecuteNonQuery();
                    amountchecked++;
                    Invoke(new MethodInvoker(delegate()
                    {
                        toolStripProgressBar1.Value = (int)((100 * amountchecked / data.Count()));
                        toolStripStatusLabel1.Text = "Amount Imported: " + amountchecked.ToString() + "/" + data.Count();
                    }));
                }
            }
            rewriteFile();
            Invoke(new MethodInvoker(delegate()
                {
                    statusStrip1.Visible = false;
                }));
        }

        private void reloadTextFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_dbConnection == null)
            {
                m_dbConnection = new SQLiteConnection("Data Source=Donators.sqlite;Version=3;");
                m_dbConnection.Open();
            }
            rewriteFile();
            log("Text files updated.");
        }

        private void manuallyChangeUserAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamTipDonationUpdater.Users users = new StreamTipDonationUpdater.Users();
            users.Show();
        }
    }
}
