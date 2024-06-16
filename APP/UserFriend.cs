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
using System.Xml.Linq;

namespace APP
{
    public partial class UserFriend : UserControl
    {
        TcpClient client;
        string userName;
        int request;
        StreamWriter sw;
        StreamReader sr;
        public UserFriend(TcpClient client, string userName)
        {
            InitializeComponent();
            this.client = client;
            this.userName = userName;
            sw = new StreamWriter(client.GetStream()); sw.AutoFlush = true;
            sr = new StreamReader(client.GetStream());

        }
        public string getname(string abc)
        {
            sw.WriteLine($"Name|{abc}");

            return sr.ReadLine();
        }
        public void setButton(string text)
        {
            bunifuButton1.Text = text;
        }
        private string _username;
        private Image _image;
        [Category("custom")]
        public string username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                lb_name.Text = $"{getname(value)}";
                lb_uname.Text = value;
            }
        }
        [Category("custom")]
        public Image userimage
        {
            get
            {
                return _image;
            }

            set
            {
                _image = value;
                PictureBox.Image = value;
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (bunifuButton1.Text == "Kết bạn")
            {
                bunifuButton1.Text = "Đã gửi";
                StreamWriter sw = new StreamWriter(client.GetStream());
                sw.AutoFlush = true;
                sw.WriteLine("AddFriend");
                //tên người gửi
                sw.WriteLine(userName);
                //tên người nhận
                sw.WriteLine(username);
            }
            else if (bunifuButton1.Text == "Chấp nhận")
            {
                StreamWriter sw = new StreamWriter(client.GetStream());
                sw.AutoFlush = true;
                sw.WriteLine("Accepted");
                //tên người gửi
                sw.WriteLine(userName);
                //tên người nhận
                sw.WriteLine(username);
            }
        }

        private void Name2_Click(object sender, EventArgs e)
        {

        }
    }
}