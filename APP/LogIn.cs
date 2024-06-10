using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace APP
{
    public partial class LogIn : Form
    {
        public class Data
        {
            public string email { get; set; }
            public string password { get; set; }
            public string userName { get; set; }
        }
        public LogIn()
        {
            InitializeComponent();
           
        }
        private TcpClient tcpClient;
        StreamWriter writer;
        StreamReader reader;
        private void Baoloi()
        {
            label7.Visible = true;
        }
        private void ConnectToServer()
        {
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(IPAddress.Parse("127.0.0.1"), 8080);

            }
            catch
            {
                MessageBox.Show("Máy chủ không hoạt động!");
                return;
            }
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;
        }
        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (tcpClient != null)
            {
                tcpClient.Close();
            }
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (bunifuTextBox2.Text == "" || bunifuTextBox2.Text == "")
            {
                Baoloi();
                bunifuTextBox2.Clear();
                bunifuTextBox1.Clear();
                return;
            }
            // tạo thread để gửi và nhận thông tin đăng nhập

            Thread threadLogin = new Thread(Login);
            threadLogin.Start();
            threadLogin.IsBackground = true;
        }
        string username, email;
        private void Login()
        {
            ConnectToServer();
            Data login = new Data()
            {
                email = bunifuTextBox1.Text,
                password = bunifuTextBox2.Text,
            };
            string loginString = JsonConvert.SerializeObject(login);
            writer.WriteLine("Login");
            writer.WriteLine(loginString);

            // đọc phản hồi từ server

            string resFromServer = reader.ReadLine();
            username = resFromServer.Substring(0, resFromServer.IndexOf(":"));
            string response = resFromServer.Substring(resFromServer.IndexOf(":") + 1);
            email = login.email;

            if (response.CompareTo("Login successfully") == 0)
            {
                Invoke(new Action(() =>
                {
                    Chat chat = new Chat(tcpClient, username,email);
                    chat.Show();
                    this.Hide();
                }));
            }
            else
            {
                // hiển thị label báo lỗi
                Invoke(new Action(() =>
                {
                    Baoloi();
                    bunifuTextBox1.Clear();
                    bunifuTextBox2.Clear();
                }));
            }
        }
        private void bunifuButton1_Click_1(object sender, EventArgs e)
        {
            Thread DKThread = new Thread(() => Application.Run(new DK()));
            DKThread.ApartmentState = ApartmentState.STA;
            DKThread.Start(); 
            this.Close();
            /*DK dK = new DK(tcpClient);
            dK.Show();
            this.Hide();*/
        }

        private void LogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void bunifuLabel1_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bunifuTextBox1.Text))
            {
                Thread forgotThread = new Thread(ForgotPass);
                forgotThread.Start();
                forgotThread.IsBackground = true;
            }
            else
            {
                MessageBox.Show("Vui lòng điền email");
            }
        }
        private void ForgotPass()
        {
            Data user = new Data()
            {
                email = bunifuTextBox1.Text,

            };
            writer.WriteLine("ForgotPass");
            string forgotstring = JsonConvert.SerializeObject(user);
            writer.WriteLine(forgotstring);
            string resFromServer = reader.ReadLine();
            string userName = resFromServer.Substring(0, resFromServer.IndexOf(":"));
            string response = resFromServer.Substring(resFromServer.IndexOf(":") + 1);
            if (response.CompareTo("A_to_reset") == 0)
            {
                Invoke(new Action(() =>
                {
                    LostPassword form = new LostPassword(userName, user.email, writer, reader);
                    form.ShowDialog();
                    this.Hide();
                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    Baoloi();
                }));
            }

        }
    }
}
