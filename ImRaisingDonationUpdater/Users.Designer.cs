namespace StreamTipDonationUpdater
{
    partial class Users
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelBox1 = new VibeLander.PanelBox();
            this.button1 = new VibeLander.ButtonBlue();
            this.tbName = new VibeLander.TxtBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAmount = new VibeLander.TxtBox();
            this.buttonBlue1 = new VibeLander.ButtonBlue();
            this.panelBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBox1
            // 
            this.panelBox1.Controls.Add(this.label2);
            this.panelBox1.Controls.Add(this.tbAmount);
            this.panelBox1.Controls.Add(this.buttonBlue1);
            this.panelBox1.Controls.Add(this.label1);
            this.panelBox1.Controls.Add(this.tbName);
            this.panelBox1.Controls.Add(this.button1);
            this.panelBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBox1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.panelBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelBox1.Location = new System.Drawing.Point(0, 0);
            this.panelBox1.Name = "panelBox1";
            this.panelBox1.NoRounding = false;
            this.panelBox1.Size = new System.Drawing.Size(278, 157);
            this.panelBox1.TabIndex = 18;
            this.panelBox1.Text = "------------------";
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Arial", 10F);
            this.button1.Image = null;
            this.button1.Location = new System.Drawing.Point(185, 38);
            this.button1.Name = "button1";
            this.button1.NoRounding = false;
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Search";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbName
            // 
            this.tbName.BackColor = System.Drawing.Color.White;
            this.tbName.Image = null;
            this.tbName.Location = new System.Drawing.Point(12, 34);
            this.tbName.MaxLength = 32767;
            this.tbName.Name = "tbName";
            this.tbName.NoRounding = false;
            this.tbName.Size = new System.Drawing.Size(166, 31);
            this.tbName.TabIndex = 6;
            this.tbName.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbName.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Donation Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(12, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Donation Amount:";
            // 
            // tbAmount
            // 
            this.tbAmount.BackColor = System.Drawing.Color.White;
            this.tbAmount.Enabled = false;
            this.tbAmount.Image = null;
            this.tbAmount.Location = new System.Drawing.Point(12, 101);
            this.tbAmount.MaxLength = 32767;
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.NoRounding = false;
            this.tbAmount.Size = new System.Drawing.Size(166, 31);
            this.tbAmount.TabIndex = 9;
            this.tbAmount.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbAmount.UseSystemPasswordChar = false;
            // 
            // buttonBlue1
            // 
            this.buttonBlue1.Enabled = false;
            this.buttonBlue1.Font = new System.Drawing.Font("Arial", 10F);
            this.buttonBlue1.Image = null;
            this.buttonBlue1.Location = new System.Drawing.Point(185, 105);
            this.buttonBlue1.Name = "buttonBlue1";
            this.buttonBlue1.NoRounding = false;
            this.buttonBlue1.Size = new System.Drawing.Size(75, 23);
            this.buttonBlue1.TabIndex = 8;
            this.buttonBlue1.Text = "Update";
            this.buttonBlue1.Click += new System.EventHandler(this.buttonBlue1_Click);
            // 
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 157);
            this.Controls.Add(this.panelBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Users";
            this.Text = "Users";
            this.panelBox1.ResumeLayout(false);
            this.panelBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private VibeLander.PanelBox panelBox1;
        private VibeLander.ButtonBlue button1;
        private VibeLander.TxtBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private VibeLander.TxtBox tbAmount;
        private VibeLander.ButtonBlue buttonBlue1;
    }
}