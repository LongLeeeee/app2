using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace APP
{
    class loginData
    {
        public string code { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
    public class socketService
    {
        private Socket clientSocket;
        public Socket GetSocket()
        {
            return this.clientSocket;
        }
        public void connect()
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect("127.0.0.1", 8080);
                //MessageBox.Show("Connected to server");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}");
            }
        }
        public void disconnect()
        {
            string text = "quit";
            clientSocket.Send(Encoding.UTF8.GetBytes(text));
            clientSocket.Close();
        }
        public void login(string Email, string Password)
        {
            try
            {
                //Tạo 1 đối tượng login 
                loginData lg = new loginData { code = "123", email = Email, password = Password };
                //convert từ kiểu loginDData => string 
                string dataString = JsonConvert.SerializeObject(lg);
                //gửi data login tới server
                byte[] data = Encoding.UTF8.GetBytes(dataString);

                clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login Failed");

            }
        }
        public void singIn(string email, string password, string username, string cpassword)
        {
            try
            {
                if (password.CompareTo(cpassword) == 0)
                {
                    loginData lg = new loginData { code = "111", email = email, password = password, username = username };
                    string dataString = JsonConvert.SerializeObject(lg);

                    byte[] data = Encoding.UTF8.GetBytes(dataString);

                    clientSocket.Send(data);
                }
                else
                {
                    MessageBox.Show("Password va ConfirmPassword chua trung khop");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void loadData()
        {
            try
            {
                string code = "110";
                byte[] data = Encoding.UTF8.GetBytes(code);
                clientSocket.Send(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void send(string mess)
        {
            try
            {
                clientSocket.Send(Encoding.UTF8.GetBytes(mess));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string receive()
        {
            byte[] recData = new byte[1024];
            clientSocket.Receive(recData);
            string resData = Encoding.UTF8.GetString(recData);
            return resData;
        }
        public void close()
        {
            clientSocket.Close();
        }
    }
}
