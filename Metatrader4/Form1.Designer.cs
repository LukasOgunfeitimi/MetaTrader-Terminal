﻿namespace Metatrader4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            button1 = new Button();
            statusText1 = new TextBox();
            UsernameText1 = new TextBox();
            PasswordText1 = new TextBox();
            ServerText1 = new TextBox();
            ServerText2 = new TextBox();
            PasswordText2 = new TextBox();
            UsernameText2 = new TextBox();
            statusText2 = new TextBox();
            button2 = new Button();
            groupBox1 = new GroupBox();
            terminalText = new TextBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            orderText = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(6, 251);
            button1.Name = "button1";
            button1.Size = new Size(224, 36);
            button1.TabIndex = 0;
            button1.Text = "Connect parent";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // statusText1
            // 
            statusText1.Location = new Point(19, 35);
            statusText1.Name = "statusText1";
            statusText1.ReadOnly = true;
            statusText1.Size = new Size(211, 34);
            statusText1.TabIndex = 1;
            statusText1.Text = "Status: Waiting";
            statusText1.TextAlign = HorizontalAlignment.Center;
            // 
            // UsernameText1
            // 
            UsernameText1.Location = new Point(19, 86);
            UsernameText1.Name = "UsernameText1";
            UsernameText1.Size = new Size(211, 34);
            UsernameText1.TabIndex = 2;
            UsernameText1.Text = "12686389";
            // 
            // PasswordText1
            // 
            PasswordText1.Location = new Point(19, 136);
            PasswordText1.Name = "PasswordText1";
            PasswordText1.Size = new Size(211, 34);
            PasswordText1.TabIndex = 3;
            PasswordText1.Text = "jycf51";
            // 
            // ServerText1
            // 
            ServerText1.Location = new Point(19, 193);
            ServerText1.Name = "ServerText1";
            ServerText1.Size = new Size(211, 34);
            ServerText1.TabIndex = 4;
            ServerText1.Text = "ICMarketsSC-Demo01";
            // 
            // ServerText2
            // 
            ServerText2.Location = new Point(25, 193);
            ServerText2.Name = "ServerText2";
            ServerText2.Size = new Size(211, 34);
            ServerText2.TabIndex = 9;
            ServerText2.Text = "ICMarketsSC-Demo01";
            // 
            // PasswordText2
            // 
            PasswordText2.Location = new Point(25, 136);
            PasswordText2.Name = "PasswordText2";
            PasswordText2.Size = new Size(211, 34);
            PasswordText2.TabIndex = 8;
            PasswordText2.Text = "roos53";
            // 
            // UsernameText2
            // 
            UsernameText2.Location = new Point(25, 86);
            UsernameText2.Name = "UsernameText2";
            UsernameText2.Size = new Size(211, 34);
            UsernameText2.TabIndex = 7;
            UsernameText2.Text = "12686391";
            // 
            // statusText2
            // 
            statusText2.Location = new Point(25, 35);
            statusText2.Name = "statusText2";
            statusText2.ReadOnly = true;
            statusText2.Size = new Size(211, 34);
            statusText2.TabIndex = 6;
            statusText2.Text = "Status: Waiting";
            statusText2.TextAlign = HorizontalAlignment.Center;
            // 
            // button2
            // 
            button2.Location = new Point(25, 251);
            button2.Name = "button2";
            button2.Size = new Size(211, 36);
            button2.TabIndex = 5;
            button2.Text = "Connect child";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(terminalText);
            groupBox1.Location = new Point(312, 305);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(608, 137);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Network Terminal";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // terminalText
            // 
            terminalText.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            terminalText.Location = new Point(55, 33);
            terminalText.Multiline = true;
            terminalText.Name = "terminalText";
            terminalText.Size = new Size(501, 85);
            terminalText.TabIndex = 0;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ServerText2);
            groupBox2.Controls.Add(statusText2);
            groupBox2.Controls.Add(UsernameText2);
            groupBox2.Controls.Add(PasswordText2);
            groupBox2.Controls.Add(button2);
            groupBox2.Location = new Point(968, 30);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(247, 348);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Child";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(statusText1);
            groupBox3.Controls.Add(ServerText1);
            groupBox3.Controls.Add(button1);
            groupBox3.Controls.Add(UsernameText1);
            groupBox3.Controls.Add(PasswordText1);
            groupBox3.Location = new Point(12, 21);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(248, 321);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Parent";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(orderText);
            groupBox4.Location = new Point(312, 21);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(608, 278);
            groupBox4.TabIndex = 13;
            groupBox4.TabStop = false;
            groupBox4.Text = "Order Terminal";
            // 
            // orderText
            // 
            orderText.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            orderText.Location = new Point(55, 33);
            orderText.Multiline = true;
            orderText.Name = "orderText";
            orderText.Size = new Size(501, 224);
            orderText.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1248, 454);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Trade Copier";
            FormClosing += Form1_Close;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        public TextBox statusText1;
        public TextBox UsernameText1;
        public TextBox PasswordText1;
        public TextBox ServerText1;
        public TextBox ServerText2;
        public TextBox PasswordText2;
        public TextBox UsernameText2;
        public TextBox statusText2;
        private Button button2;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private TextBox terminalText;
        private GroupBox groupBox4;
        private TextBox orderText;
    }
}
