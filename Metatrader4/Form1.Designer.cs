namespace Metatrader4
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
        private void InitializeComponent()
        {
            button1 = new Button();
            statusText = new TextBox();
            UsernameText = new TextBox();
            PasswordText = new TextBox();
            ServerText = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(22, 61);
            button1.Name = "button1";
            button1.Size = new Size(134, 36);
            button1.TabIndex = 0;
            button1.Text = "Start parent";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // statusText
            // 
            statusText.Location = new Point(22, 12);
            statusText.Name = "statusText";
            statusText.ReadOnly = true;
            statusText.Size = new Size(163, 34);
            statusText.TabIndex = 1;
            statusText.Text = "Status: Waiting";
            statusText.TextChanged += statusText_TextChanged;
            // 
            // UsernameText
            // 
            UsernameText.Location = new Point(22, 113);
            UsernameText.Name = "UsernameText";
            UsernameText.Size = new Size(134, 34);
            UsernameText.TabIndex = 2;
            // 
            // PasswordText
            // 
            PasswordText.Location = new Point(22, 170);
            PasswordText.Name = "PasswordText";
            PasswordText.PasswordChar = '*';
            PasswordText.Size = new Size(134, 34);
            PasswordText.TabIndex = 3;
            // 
            // ServerText
            // 
            ServerText.Location = new Point(22, 228);
            ServerText.Name = "ServerText";
            ServerText.PasswordChar = '*';
            ServerText.Size = new Size(134, 34);
            ServerText.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(881, 450);
            Controls.Add(ServerText);
            Controls.Add(PasswordText);
            Controls.Add(UsernameText);
            Controls.Add(statusText);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Trade Copier";
            Load += Form1_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        public TextBox statusText;
        public TextBox UsernameText;
        public TextBox PasswordText;
        public TextBox ServerText;
    }
}
