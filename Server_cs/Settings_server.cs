using System;
using System.Windows.Forms;

namespace Server_cs
{
    public partial class Settings_server : Form
    {
        public Settings_server()
        {
            InitializeComponent();
        }

        private void Settings_server_Activated(object sender, EventArgs e)
        {
            port_edit.Text = Form1.SERVER_PORT.ToString();
            textBox1.Text = Form1.PATH_LOGFILE;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int port = int.Parse(port_edit.Text);
            if ( port> 10000 || port<0)
            {
                MessageBox.Show("Неверное значение", "Ошибка!");
                return;
            }
            Form1.SERVER_PORT = port;
            Form1.PATH_LOGFILE = textBox1.Text;
            Close();
        }

        private void port_edit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) & (e.KeyChar != (char)Keys.Back))
                e.Handled = true;
        }
    }
}
