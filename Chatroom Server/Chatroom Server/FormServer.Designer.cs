namespace Chatroom_Server
{
    partial class Server
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
            btnSend = new Button();
            txtMessage = new TextBox();
            lsvMessage = new ListView();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new Point(307, 324);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(121, 97);
            btnSend.TabIndex = 5;
            btnSend.Text = "SEND";
            btnSend.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(12, 324);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(268, 97);
            txtMessage.TabIndex = 4;
            // 
            // lsvMessage
            // 
            lsvMessage.Location = new Point(12, 12);
            lsvMessage.Name = "lsvMessage";
            lsvMessage.Size = new Size(416, 306);
            lsvMessage.TabIndex = 3;
            lsvMessage.UseCompatibleStateImageBehavior = false;
            lsvMessage.View = View.List;
            // 
            // Server
            // 
            AcceptButton = btnSend;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(506, 440);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(lsvMessage);
            Name = "Server";
            Text = "Server";
            Load += Server_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private TextBox txtMessage;
        private ListView lsvMessage;
    }
}
