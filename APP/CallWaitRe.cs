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
    public partial class CallWaitRe : Form
    {
        StreamWriter Writer;
        StreamReader Reader;
        TcpClient Client;
        string Username;
        string Sender;
        Call CallForm;
        public CallWaitRe(string sender, TcpClient client, StreamWriter writer, StreamReader reader, string username)
        {
            InitializeComponent();

            bunifuLabel1.Text = sender;
            Writer = writer;
            Reader = reader;
            Client = client;
            Username = username;
            Sender = sender;
        }

        private void close_Click(object sender, EventArgs e)
        {
            Writer.AutoFlush = true;
            Writer.WriteLine("NguoiNhanCup");
            Writer.WriteLine(Username + "|" + Sender);
            this.Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Writer.AutoFlush = true;
            Writer.WriteLine("NguoiNhanChapNhan");
            Writer.WriteLine(Username + "|" + Sender);
            
            
            this.Close();
        }
    }
}
