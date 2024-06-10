using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP
{
    public partial class CallWaitSe : Form
    {
        private Timer timer;
        private int remainingTime;
        string Username;
        StreamWriter Writer;
        StreamReader Reader;
        TcpClient Client;
        string Talkwith;
        public CallWaitSe(string Twith, TcpClient client, StreamWriter writer, StreamReader reader, string username )
        {
            InitializeComponent();
            InitializeCountdownTimer();
            Talkwith = Twith;
            bunifuLabel1.Text = Talkwith;
            bunifuLabel2.Text = "Đang gọi";
            Writer = writer;
            Reader = reader;
            Client = client;
            Username = username;
        }

        private void bunifuPanel3_Click(object sender, EventArgs e)
        {

        }
        private void InitializeCountdownTimer()
        {
            remainingTime = 30; // 60 giây

            timer = new Timer();
            timer.Interval = 1000; // 1 giây
            timer.Tick += Timer_Tick;
            timer.Start();

            
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime--;
            

            if (remainingTime <= 0)
            {
                timer.Stop();
                Writer.AutoFlush = true;
                Writer.WriteLine("NguoiGoiCup");
                Writer.WriteLine(Username + "|" + Talkwith);
                this.Close(); // Đóng form
            }

        }

        private void close_Click(object sender, EventArgs e)
        {
            Writer.AutoFlush = true;
            Writer.WriteLine("NguoiGoiCup");
            Writer.WriteLine(Username + "|" + Talkwith);
            this.Close();
        }
    }
}
