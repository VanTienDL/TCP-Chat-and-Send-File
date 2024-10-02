namespace ChatRoom
{
    partial class Client
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
            lsvMessage = new ListView();
            txtMessage = new TextBox();
            btnSend = new Button();
            SuspendLayout();
            // 
            // lsvMessage
            // 
            lsvMessage.Location = new Point(12, 12);
            lsvMessage.Name = "lsvMessage";
            lsvMessage.Size = new Size(416, 306);
            lsvMessage.TabIndex = 0;
            lsvMessage.UseCompatibleStateImageBehavior = false;
            lsvMessage.View = View.List;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(12, 341);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(268, 97);
            txtMessage.TabIndex = 1;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(300, 341);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(121, 97);
            btnSend.TabIndex = 2;
            btnSend.Text = "SEND";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // Client
            // 
            AcceptButton = btnSend;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 450);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(lsvMessage);
            Name = "Client";
            Text = "Client";
            Load += Client_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lsvMessage;
        private TextBox txtMessage;
        private Button btnSend;
    }
}
