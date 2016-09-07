using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net;

namespace Server_cs
{
    public struct Message
    {
        public string from;
        public string to;
        public string mes;
        public bool read { get; set; }
        public Message (string []m)
        {
            from = m[1];
            to = m[0];
            mes = m[2];
            read = false;
        }
      
    }

    public partial class Form1 : Form
    {
        //объявление объектов класса
        TcpListener tcplistener;
        Thread listenThread;
        
        internal static int SERVER_PORT = 12000;
        int CONNECT_COUNT = 0;
        internal static string PATH_LOGFILE = "ServerFiles\\log.txt";
        StreamWriter sw = new StreamWriter(PATH_LOGFILE, true);
        List<string> users = new List<string>();
        List<Message> list_m = new List<Message>();

        //По умолчанию сервер запущен
        public Form1()
        {
            InitializeComponent();
            Server();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sw.Close();
            Close();
        }

        //Функция запуска сервера
        private void Server()
        {
            //Создаем объект
            tcplistener = new TcpListener(IPAddress.Any, SERVER_PORT);
            //Создаем вторичный поток
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
        }

        private void ListenForClients()
        {
            tcplistener.Start(); // <-- Запуск сервера
            while (true)
            {
                try
                {
                    TcpClient client = tcplistener.AcceptTcpClient();
                    Thread clientthread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientthread.Start(client);
                }
                catch
                {
                    break;
                }
            }

        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpclient = (TcpClient)client;
            NetworkStream clientStream = tcpclient.GetStream();
            Socket s = tcpclient.Client;
            IPEndPoint ep = (IPEndPoint)s.RemoteEndPoint;
            IPAddress addr = ep.Address;

            //Запись в журнал соединений
            string str = addr.ToString();
            DateTime dt = DateTime.Now;
            string strdt = dt.ToString("G"); // 08.02.2016
            str += " " + strdt+" ";
            sw.Write(str);
            sw.Flush(); // сброс буферов
            CONNECT_COUNT++;


            //Получение сообщение и отправка ответа

            ///*** ПОЛУЧЕНИЕ
            byte[] message = new byte[4096];
            int bytesRead;
            while (true)
            {
                bytesRead = 0;
                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {   //сообщение об ошибке
                    break;
                }

            ///*** ОТВЕТ
                UTF8Encoding encoder = new UTF8Encoding();
                string msg = encoder.GetString(message, 0, bytesRead);
                Echo(msg, encoder, clientStream);
            }
            tcpclient.Close();
        }

        //Дерево ответов
        private void Echo(string msg, UTF8Encoding encoder, NetworkStream clientStream)
        {
            if (msg.StartsWith("LOGIN^"))
            {
                msg = msg.TrimStart("LOGIN^".ToCharArray());
                if (users.Contains(msg))
                {
                    Random r = new Random();
                    msg += r.Next(100000);
                }
                users.Add(msg);
                sw.WriteLine(msg);
                sw.Flush();
            }
            else if (msg == "GET_USERS")
            {
                msg = "";
                for(int i=0; i<users.Count; i++)
                {
                    msg += users[i] + "^";
                }
               // msg = "LIST_OF_USERS";
            }
            else if (msg.StartsWith("GET^"))
            {
                msg = msg.TrimStart("GET^".ToCharArray());
                string list_mess = "";

                List<int> ind = new List<int>();
                for (int i=0; i< list_m.Count; i++)
                {
                    if (list_m[i].to == msg && list_m[i].read == false)
                    {
                        list_mess += list_m[i].from + "^" + list_m[i].mes + "^";
                        ind.Add(i-ind.Count);
                        continue;
                    }
                }
                foreach (int i in ind)
                {
                    
                    list_m.RemoveAt(i);
                }
                msg = list_mess;
                if (msg == "")
                {
                    msg = "ERROR";
                }
            }
            else if (msg.StartsWith("CLOSE^"))
            {
                CONNECT_COUNT--;
                msg = msg.TrimStart("CLOSE^".ToCharArray());
                
                int ind = users.IndexOf(msg);
                if (ind >= 0)
                    users.RemoveAt(ind);
                
               
            }
            else
            {
               //
                Message buf_m = new Message(msg.Split('^'));
                
                list_m.Add(buf_m);
                //msg = "MSG_SEND";
                return;
            }
            byte[] buffer = encoder.GetBytes(msg);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }

        private void журналСоединенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str;
            richTextBox1.Clear();
            sw.Close();
            StreamReader sr = new StreamReader(PATH_LOGFILE);
            while (!sr.EndOfStream)
            {
                str = sr.ReadLine();
                richTextBox1.AppendText(str + "\n");
            }
            sr.Close();
            sw = new StreamWriter(PATH_LOGFILE, true);
            ConnectCount_edit.Text = CONNECT_COUNT.ToString();
        }

        private void остановитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcplistener.Stop();
            остановитьToolStripMenuItem.Enabled = false;
            запуститьToolStripMenuItem.Enabled = true;
            настройкиToolStripMenuItem.Enabled = true;
        }

        private void запуститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Server();
            настройкиToolStripMenuItem.Enabled = false;
            запуститьToolStripMenuItem.Enabled = false;
            остановитьToolStripMenuItem.Enabled = true;
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings_server set_sv = new Settings_server();
            set_sv.ShowDialog();
        }

    }
       
}
