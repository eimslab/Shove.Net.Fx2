using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Security.Cryptography;
using LumiSoft.Net.TCP;
using LumiSoft.Net;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Shove._Net.TCP.SocketService.Server SocketServer = new Shove._Net.TCP.SocketService.Server("localhost", "127.0.0.1", 3000, new Shove._Net.TCP.SocketService.Server.ReceiveHandler(ReceiveHandler1));
            string ErrorDescroption = "";

            if (!SocketServer.Start(ref ErrorDescroption))
            {
                MessageBox.Show(ErrorDescroption);

                return;
            }
        }

        public byte[] ReceiveHandler1(byte[] input)
        {
            byte[] n = System.Text.Encoding.Default.GetBytes("9876543210aaaaaaaa12343t1234567中国89@#$%^&*()ertyuiopsdfghjkEND*()))ooooooiRTYU");

            return n;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Shove._Net.TCP.SocketService.Client SocketClient = new Shove._Net.TCP.SocketService.Client("127.0.0.1", 3000);
            //string ErrorDescroption = "";

            //if (!SocketClient.Connect(ref ErrorDescroption))
            //{
            //    MessageBox.Show(ErrorDescroption);

            //    return;
            //}

            //byte[] r = System.Text.Encoding.Default.GetBytes("9876543210aaaaaaaa12343t1234567中国89@#$%^&*()ertyuiopsdfghjkEND*()))ooooooiRTYU");

            //if (!SocketClient.SendData(r, ref ErrorDescroption))
            //{
            //    MessageBox.Show(ErrorDescroption);

            //    return;
            //}

            //r = SocketClient.ReceiveData(ref ErrorDescroption);

            //MessageBox.Show(r.Length.ToString() + "," + System.Text.Encoding.Default.GetString(r));

            int Count = int.Parse(textBox5.Text);
            pbar.Maximum = Count - 1;

            for (int JJ = 0; JJ < Count; JJ++)
            {
                TCP_Client tcpClient = new TCP_Client();
                try
                {
                    tcpClient.Connect(textBox6.Text, int.Parse(textBox7.Text));
                }
                catch (Exception ex)
                {
                    textBox1.Text = ex.Message;

                    return;
                }

                if (!tcpClient.IsConnected)
                {
                    textBox1.Text = "没有连接到远程服务器。";

                    return;
                }
                else
                {
                    textBox1.Text = "连接成功了！";
                    this.Update();
                }

                //tcpClient.TcpStream.ReadTimeout = 5000;

                byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(textBox3.Text);
                tcpClient.TcpStream.Write(buffer, 0, buffer.Length);
                labSendTip.Text = "已发送 " + buffer.Length.ToString() + " 个字节数据。";
                this.Update();

                /////////////////////////////////
                tcpClient.TcpStream.Read(buffer, 0, 10);
                textBox4.Text = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
                this.Update();

                pbar.Value = JJ;
                this.Update();

                ////////////////////////////////
                tcpClient.Disconnect();

                //System.Threading.Thread.Sleep(500);
            }

            MessageBox.Show("测试结束。");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //MyThread mt = new MyThread(this);
            //mt.Run();

            int Count = int.Parse(textBox5.Text);
            pbar.Maximum = Count - 1;

            for (int JJ = 0; JJ < Count; JJ++)
            {
                TCP_Client tcpClient = new TCP_Client();
                try
                {
                    tcpClient.Connect(textBox6.Text, int.Parse(textBox7.Text));
                }
                catch (Exception ex)
                {
                    textBox1.Text = ex.Message;

                    return;
                }

                if (!tcpClient.IsConnected)
                {
                    textBox1.Text = "没有连接到远程服务器。";

                    return;
                }
                else
                {
                    textBox1.Text = "连接成功了！";
                    this.Update();
                }

                //tcpClient.TcpStream.ReadTimeout = 5000;

                ////////////////////////////////
                byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(textBox3.Text);
                byte[] b2 = new byte[4 + buffer.Length];
                byte[] b3 = System.BitConverter.GetBytes(buffer.Length);
                for (int i = 0; i < 4; i++)
                {
                    b2[i] = b3[i];
                }
                for (int i = 0; i < buffer.Length; i++)
                {
                    b2[i + 4] = buffer[i];
                }
                tcpClient.TcpStream.Write(b2, 0, b2.Length);
                labSendTip.Text = "已发送 " + buffer.Length.ToString() + " 个字节数据。";
                this.Update();

                /////////////////////////////////
                byte[] b = new byte[4];
                tcpClient.TcpStream.Read(b, 0, 4);
                int read_len = System.BitConverter.ToInt32(b, 0);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                tcpClient.TcpStream.ReadFixedCount(ms, read_len);

                buffer = ms.ToArray();
                textBox4.Text = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
                labRevTip.Text = "已接收到 " + read_len.ToString() + " 个字节数据。";
                pbar.Value = JJ;
                this.Update();

                ////////////////////////////////
                tcpClient.Disconnect();

                //System.Threading.Thread.Sleep(500);
            }

            MessageBox.Show("测试结束。");
        }

        TCP_Server<TCP_ServerSession> tcpServer = new TCP_Server<TCP_ServerSession>();

        private void button4_Click(object sender, EventArgs e)
        {
            tcpServer.Bindings = new LumiSoft.Net.IPBindInfo[] { new IPBindInfo("192.168.1.8", LumiSoft.Net.BindInfoProtocol.TCP, System.Net.IPAddress.Parse("192.168.1.8"), 3000) };
            tcpServer.SessionCreated += new EventHandler<TCP_ServerSessionEventArgs<TCP_ServerSession>>(tcpServer_SessionCreated);
            tcpServer.Start();
        }

        protected void tcpServer_SessionCreated(object sender, TCP_ServerSessionEventArgs<TCP_ServerSession> e)
        {
            byte[] buffer = System.Text.ASCIIEncoding.ASCII.GetBytes("Hello, welcome to the EIMS3 Server.");
            byte[] b2 = new byte[4 + buffer.Length];
            byte[] b3 = System.BitConverter.GetBytes(buffer.Length);
            for (int i = 0; i < 4; i++)
            {
                b2[i] = b3[i];
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                b2[i + 4] = buffer[i];
            }
            e.Session.TcpStream.Write(b2, 0, b2.Length);

            //////////////////////////////////////////

            byte[] b = new byte[4];
            e.Session.TcpStream.Read(b, 0, 4);
            int read_len = System.BitConverter.ToInt32(b, 0);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            e.Session.TcpStream.ReadFixedCount(ms, read_len);

            buffer = ms.ToArray();
            
            //////////////////////////////////////////

            b2 = new byte[4 + buffer.Length + 1];
            b3 = System.BitConverter.GetBytes(buffer.Length);
            for (int i = 0; i < 4; i++)
            {
                b2[i] = b3[i];
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                b2[i + 4] = buffer[i];
            }
            b2[4 + buffer.Length] = (int)'!';

            e.Session.TcpStream.Write(b2, 0, b2.Length);
            e.Session.TcpStream.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tcpServer.Stop();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string aa = Shove._Security.Encrypt.EncryptSES("abcdefghijk", "qwertyu1qwertyu3qwertyu6", Encoding.UTF8.EncodingName);
            textBox4.Text = aa + "\r\n";

            aa = Shove._Security.Encrypt.DecryptSES(aa, "qwertyu1qwertyu3qwertyu6", Encoding.UTF8.EncodingName);
            textBox4.Text += aa;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable("t_users");

            //dt.Columns.Add(new DataColumn("id", typeof(int)));
            //dt.Columns.Add(new DataColumn("name", typeof(string)));

            //DataRow dr = dt.NewRow();
            //dr["id"] = 1;
            //dr["name"] = "张三";

            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["id"] = 2;
            //dr["name"] = "李四";

            //dt.Rows.Add(dr);

            //dt.AcceptChanges();

            //textBox4.Text = Shove._Convert.DataTableToXML(dt);





            //ShoveEIMS3.SystemConfig sc = new ShoveEIMS3.SystemConfig();

            //textBox4.Text = sc.soft_owner_id + "," + sc.soft_type + "," + sc.default_form + "," + sc.is_actived;

            //sc.soft_owner_id = 123456;
            //sc.soft_type = 2;
            //sc.default_form = 20;
            //sc.is_actived = 1;

            //sc.Save();

            //sc = new ShoveEIMS3.SystemConfig();

            //textBox4.Text += "\r\n" + sc.soft_owner_id + "," + sc.soft_type + "," + sc.default_form + "," + sc.is_actived;


            int a1 = 0;
            string a2 = "1";

            object[] pp = new object[2];
            pp[0] = a1;
            pp[1] = a2;

            aaaa(pp);

            MessageBox.Show(sizeof(short).ToString() + "," + sizeof(bool).ToString());

        }

        public void aaaa(object[] pp)
        {
            pp[0] = 20;
            pp[1] = "sssssss";
        }
    }
}

/*
using System;
using System.Runtime.InteropServices;

public class UndocumentedCSharp
{
  private static void Show(__arglist)
  {
    ArgIterator it = new ArgIterator(__arglist);

    while(it.GetRemainingCount() >0)
   {
   TypedReference tr = it.GetNextArg();

   Console.Out.WriteLine("{0}: {1}", TypedReference.ToObject(tr), __reftype(tr));
   }
  }

  public static void Main(String[] args)
  {
    Show(__arglist("Flier Lu", 1024));
  }
}
*/