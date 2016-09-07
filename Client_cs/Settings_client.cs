using System;
using System.Windows.Forms;

namespace Client_cs
{
    public partial class Settings_client : Form
    {
        public Settings_client()
        {
            InitializeComponent();
        }

        private void Connect_btn_Click(object sender, EventArgs e)
        {
            Form1.CLIENT_PORT = int.Parse( Port_edit.Text);
            Form1.CLIENT_HOST = Host_edit.Text;
            Form1.CLIENT_LOGIN = login_edit.Text;
        }

        private void Settings_client_Activated(object sender, EventArgs e)
        {
            Port_edit.Text = Form1.CLIENT_PORT.ToString();
            Host_edit.Text = Form1.CLIENT_HOST;
            login_edit.Text = Form1.CLIENT_LOGIN;
        }

        private void Port_edit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 48 || e.KeyChar >= 59) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
