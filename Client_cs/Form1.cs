using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Client_cs
{
    public partial class Form1 : Form
    {
        internal static int CLIENT_PORT = 12000;
        internal static string CLIENT_HOST = "127.0.0.1";
        internal static string CLIENT_LOGIN = "user";
        TcpClient client = new TcpClient();
        IPEndPoint serverEndPoint;
        public Form1()
        {
            InitializeComponent();
        }

        private void соединитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                Settings_client set_cl = new Settings_client();
                if (set_cl.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                //Создаем удаленную точку для сервера
                serverEndPoint = new IPEndPoint(IPAddress.Parse(CLIENT_HOST), CLIENT_PORT);
                client.Connect(serverEndPoint);
               
                //Сразу посылаем логин пользователя
                NetworkStream clientStream = client.GetStream();
                //Объект кодировщика
                UTF8Encoding encoder = new UTF8Encoding();

                byte[] buf = encoder.GetBytes("LOGIN^"+CLIENT_LOGIN);
                clientStream.Write(buf, 0, buf.Length);
                clientStream.Flush();
                byte[] data = new byte[256];
                string responsedata = string.Empty;
                int bytes = clientStream.Read(data, 0, data.Length);
                responsedata = Encoding.UTF8.GetString(data, 0, bytes);
                MessageBox.Show("Соединение установлено");

                отправитьСообщениеToolStripMenuItem.Enabled = true; ;
                получитьСписокПользователейToolStripMenuItem.Enabled = true;
                получитьСписокПользователейToolStripMenuItem.Enabled = true;
                получитьСообщениеToolStripMenuItem.Enabled = true;
                CloseToolStripMenuItem.Enabled = true;
                соединитьToolStripMenuItem.Enabled = false;

                CLIENT_LOGIN = responsedata;
                textBox1.Text = CLIENT_LOGIN;
            }
            catch
            {
                MessageBox.Show("Ошибка соединения. Попробуйте позднее");
            }
            
        }

        private void отправитьСообщениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = richTextBox1.Text;
           
            string send = comboBox1.Text;
            if (send == String.Empty)
            {
                return;
            }
            string receive = textBox1.Text;
           
            if (msg == String.Empty)
            {
                richTextBox1.BackColor = Color.MistyRose;
                return;
            }
            NetworkStream clientStream = client.GetStream();
            //Объект кодировщика
            UTF8Encoding encoder = new UTF8Encoding();

            msg = send + "^" + receive + "^" + msg + "^";

            byte[] buf = encoder.GetBytes(msg);
            clientStream.Write(buf, 0, buf.Length);
            clientStream.Flush();
            /*byte[] data = new byte[256];
            string responsedata = string.Empty;
            int bytes = clientStream.Read(data, 0, data.Length);
            responsedata = Encoding.UTF8.GetString(data, 0, bytes);*/
            richTextBox1.Clear();
            MessageBox.Show("Сообщение успешно отправлено");
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.White;
        }

        private void получитьСписокПользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetworkStream clientStream = client.GetStream();
            //Объект кодировщика
            UTF8Encoding encoder = new UTF8Encoding();


            byte[] buf = encoder.GetBytes("GET_USERS");
            clientStream.Write(buf, 0, buf.Length);
            clientStream.Flush();
            byte[] data = new byte[256];
            string UserList = string.Empty;
            int bytes = clientStream.Read(data, 0, data.Length);
            UserList = Encoding.UTF8.GetString(data, 0, bytes);

            string[] Users = UserList.Split('^');
            comboBox1.Items.Clear();
            for (int i=0; i< Users.Count<string>() - 1; i++)
            {
                comboBox1.Items.Add(Users[i]);

            }
            
        }

        private void получитьСообщениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            string send = textBox1.Text;
            NetworkStream clientStream = client.GetStream();
            //Объект кодировщика
            UTF8Encoding encoder = new UTF8Encoding();

            send = "GET^"+send;

            byte[] buf = encoder.GetBytes(send);
            clientStream.Write(buf, 0, buf.Length);
            clientStream.Flush();
            byte[] data = new byte[256];
            string responsedata = string.Empty;
            int bytes = clientStream.Read(data, 0, data.Length);
            responsedata = Encoding.UTF8.GetString(data, 0, bytes);
            if (responsedata == "ERROR")
            {
                MessageBox.Show("Для вас нет сообщений");
                return;
            }
            string[] answers = responsedata.Split('^');
            for (int i=0; i<answers.Count<string>()-1; i+=2)
            {
                MessageBox.Show(answers[i+1],"From: "+answers[i]);
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string send = textBox1.Text;
            if (!client.Connected)
            {
                return;
            }
            //Предупредить сервер о закрытии
            NetworkStream clientStream = client.GetStream();
            //Объект кодировщика
            UTF8Encoding encoder = new UTF8Encoding();

            send = "CLOSE^" + send;

            byte[] buf = encoder.GetBytes(send);
            clientStream.Write(buf, 0, buf.Length);
            clientStream.Flush();

//Остановить соединение
            client.Close();


            отправитьСообщениеToolStripMenuItem.Enabled = false; 
            получитьСписокПользователейToolStripMenuItem.Enabled = false;
            получитьСписокПользователейToolStripMenuItem.Enabled = false;
            получитьСообщениеToolStripMenuItem.Enabled = false;
            CloseToolStripMenuItem.Enabled = false;
            соединитьToolStripMenuItem.Enabled = true;
            richTextBox1.Clear();
            comboBox1.Items.Clear();
            textBox1.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseToolStripMenuItem_Click(sender, e);
        }
    }
}
