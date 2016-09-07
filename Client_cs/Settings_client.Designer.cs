namespace Client_cs
{
    partial class Settings_client
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
            this.Port_edit = new System.Windows.Forms.TextBox();
            this.Host_edit = new System.Windows.Forms.TextBox();
            this.login_edit = new System.Windows.Forms.TextBox();
            this.Connect_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Port_edit
            // 
            this.Port_edit.Location = new System.Drawing.Point(144, 53);
            this.Port_edit.Name = "Port_edit";
            this.Port_edit.Size = new System.Drawing.Size(164, 20);
            this.Port_edit.TabIndex = 0;
            this.Port_edit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Port_edit_KeyPress);
            // 
            // Host_edit
            // 
            this.Host_edit.Location = new System.Drawing.Point(144, 88);
            this.Host_edit.Name = "Host_edit";
            this.Host_edit.Size = new System.Drawing.Size(163, 20);
            this.Host_edit.TabIndex = 1;
            // 
            // login_edit
            // 
            this.login_edit.Location = new System.Drawing.Point(143, 129);
            this.login_edit.Name = "login_edit";
            this.login_edit.Size = new System.Drawing.Size(165, 20);
            this.login_edit.TabIndex = 2;
            // 
            // Connect_btn
            // 
            this.Connect_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Connect_btn.Location = new System.Drawing.Point(178, 181);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(97, 28);
            this.Connect_btn.TabIndex = 0;
            this.Connect_btn.Text = "Соединить";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Номер порта";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Имя хоста";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Логин";
            // 
            // Settings_client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 240);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Connect_btn);
            this.Controls.Add(this.login_edit);
            this.Controls.Add(this.Host_edit);
            this.Controls.Add(this.Port_edit);
            this.Name = "Settings_client";
            this.Text = "Настройки соединения с сервером";
            this.Activated += new System.EventHandler(this.Settings_client_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Port_edit;
        private System.Windows.Forms.TextBox Host_edit;
        private System.Windows.Forms.TextBox login_edit;
        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}