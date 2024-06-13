using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP
{
    public partial class Call : Form
    {
        private System.Windows.Forms.Timer timer;
        private DateTime startTime;
        string nguoigoi;
        string Username;
        StreamWriter W;
        StreamReader R;
        WaveIn Wa;
        
        public Call(string nguoigoi, StreamWriter writer, StreamReader reader, string username, WaveIn wavein)
        {
            InitializeComponent();
            InitializeTimer();
            nguoigoi = nguoigoi;
            Username = username;
            bunifuLabel1.Text = nguoigoi;
            W = writer;
            R = reader;
            Wa = wavein;


        }
       
        private void InitializeTimer()
        {
            startTime = DateTime.Now;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1 giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            bunifuLabel2.Text = string.Format("{0:D2}:{1:D2}", elapsedTime.Minutes, elapsedTime.Seconds);
 
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            /*Writer.AutoFlush = true;
            Writer.WriteLine("NguoiNhanChapNhan");
            Writer.WriteLine(Username + "|" + Sender);*/
            Wa.StopRecording();
            this.Close();
        }
    }
}
