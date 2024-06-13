using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Newtonsoft.Json;

namespace APP
{
    public partial class LostPassword : Form
    {
        public class Data
        {
            public string email { get; set; }
            public string password { get; set; }
            public string userName { get; set; }
        }
        string Email;
        StreamWriter writer;
        StreamReader reader;
        string resetCode;
        string username;
        public LostPassword(string username, string email, StreamWriter Writer, StreamReader Reader)
        {
            InitializeComponent();
            bunifuPanel2.Visible = false;
            Email = email;
            writer = Writer;
            reader = Reader;
            resetCode = GenerateCode();
            this.username = username;
        }
        public string GenerateCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var code = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }
        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bunifuTextBox1.Text))
            {
                if (resetCode.CompareTo(bunifuTextBox1.Text) == 0)
                {
                    bunifuPanel2.Visible = true;
                }
                else
                {
                    MessageBox.Show("Mã đặt lại mật khẩu của bạn không chính xác");
                    bunifuTextBox1.Clear();
                }
            }
            else MessageBox.Show("Vui lòng điền mã!");
        }

        private void LostPassword_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Mã đặt lại mật khẩu sẽ được gửi qua email của bạn");

            new Thread(() => SendEmail(Email, resetCode)).Start();
        }
        private void SendEmail(string toEmail, string code)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("22520985@gm.uit.edu.vn", "Ứng dụng trò chuyện trực tuyến_Nhóm 12");
                mail.To.Add(toEmail);
                mail.Subject = "OTP";
                mail.Body = $"Xin chào,\n\nMã đặt lại mật khẩu của bạn là: {code}";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("22520985@gm.uit.edu.vn", "1991419869");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

                MessageBox.Show($"Gửi Email thất bại: {ex.Message}\r\n");

            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bunifuTextBox2.Text))
            {
                if (bunifuTextBox2.Text.CompareTo(bunifuTextBox3.Text) == 0)
                {
                    Thread forgotThread = new Thread(resetPassword);
                    forgotThread.Start();
                    forgotThread.IsBackground = true;
                }
                else
                {
                    MessageBox.Show("mật khẩu xác nhận không chính xác!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng điền mật khẩu mới");
            }
        }
        private void resetPassword()
        {
            Data user = new Data()
            {
                email = Email,
                password = bunifuTextBox2.Text,
                userName = this.username
            };
            string resetstring = JsonConvert.SerializeObject(user);
            writer.WriteLine("resetPass");

            writer.WriteLine(resetstring);
            string resFromServer = reader.ReadLine();
            string response = resFromServer.Substring(resFromServer.IndexOf(":") + 1);

            if (response.CompareTo("reset_success") == 0)
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("bạn đã đặt lại mật khẩu thành công!");
                    this.Hide();
                    LogIn form = new LogIn();
                    form.Show();
                }));
            }
            else
            {
                Invoke(new Action(() =>
                {
                    MessageBox.Show("bạn đã đặt lại mật khẩu thất bại!");
                    return;
                }));
            }
        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {
            string password = bunifuTextBox2.Text;

            if (password.Length < 8)
            {
                lblPasscheck.Text = "Mật khẩu cần ít nhất 8 ký tự.";
                return;
            }
            else if (!password.Any(char.IsUpper))
            {
                lblPasscheck.Text = "Mật khẩu cần ít nhất một ký tự hoa.";
                return;
            }
            else if (!password.Any(char.IsLower))
            {
                lblPasscheck.Text = "Mật khẩu cần ít nhất một ký tự thường.";
                return;
            }
            else if (!password.Any(char.IsDigit))
            {
                lblPasscheck.Text = "Mật khẩu cần ít nhất một số.";
                return;
            }
            else
            {
                // Nếu mật khẩu đáp ứng tất cả các điều kiện, xóa thông báo lỗi
                lblPasscheck.Text = "";
            }
        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }
<<<<<<< HEAD
=======

        private void LostPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            Invoke(new Action(() =>
            {
                this.Hide();
            }));
        }
>>>>>>> 85de325 (longle)
    }
}
