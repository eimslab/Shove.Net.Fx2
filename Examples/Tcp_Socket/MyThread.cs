using System;
using System.Collections.Generic;
using System.Text;
using LumiSoft.Net.TCP;

namespace WindowsFormsApplication1
{
    public class MyThread
    {
        Form1 frmMain;
        private System.Threading.Thread thread;

        public MyThread(Form1 f)
        {
            frmMain = f;
        }

        public void Run()
        {
            lock (this) // 确保临界区被一个 Thread 所占用
            {
                thread = new System.Threading.Thread(new System.Threading.ThreadStart(Do));
                thread.IsBackground = true;

                thread.Start();
            }
        }

        public void Do()
        {
            System.Threading.Thread.Sleep(1000);   // 1秒为单位
            int Count = int.Parse(frmMain.textBox5.Text);
            frmMain.pbar.Maximum = Count - 1;


            for (int JJ = 0; JJ < Count; JJ++)
            {
                TCP_Client tcpClient = new TCP_Client();
                try
                {
                    tcpClient.Connect(frmMain.textBox6.Text, int.Parse(frmMain.textBox7.Text));
                }
                catch(Exception ex)
                {
                    frmMain.textBox1.Text = ex.Message;

                    return;
                }

                if (!tcpClient.IsConnected)
                {
                    frmMain.textBox1.Text = "没有连接到远程服务器。";

                    return;
                }
                else
                {
                    frmMain.textBox1.Text = "连接成功了！";
                    frmMain.Update();
                }

                //////////////////////////////////////////
                byte[] b = new byte[4];
                tcpClient.TcpStream.Read(b, 0, 4);
                int read_len = System.BitConverter.ToInt32(b, 0);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                tcpClient.TcpStream.ReadFixedCount(ms, read_len);

                byte[] buffer = ms.ToArray();
                frmMain.textBox2.Text = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
                frmMain.Update();

                ////////////////////////////////
                buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(frmMain.textBox3.Text);
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
                frmMain.labSendTip.Text = "已发送 " + buffer.Length.ToString() + " 个字节数据。";
                frmMain.Update();

                /////////////////////////////////
                b = new byte[4];
                tcpClient.TcpStream.Read(b, 0, 4);
                read_len = System.BitConverter.ToInt32(b, 0);

                ms = new System.IO.MemoryStream();
                tcpClient.TcpStream.ReadFixedCount(ms, read_len);

                buffer = ms.ToArray();
                frmMain.textBox4.Text = System.Text.ASCIIEncoding.ASCII.GetString(buffer);
                frmMain.labRevTip.Text = "已接收到 " + read_len.ToString() + " 个字节数据。";
                frmMain.pbar.Value = JJ;
                frmMain.Update();

                ////////////////////////////////
                tcpClient.Disconnect();
            }
        }
    }
}
