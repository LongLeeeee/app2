using Bunifu.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
<<<<<<< HEAD
=======
using System.Net.Sockets;
>>>>>>> 85de325 (longle)
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP
{
    public partial class ChangePassword : Form
    {
        string username, email;
        StreamWriter writer;
        StreamReader reader;
<<<<<<< HEAD
        public ChangePassword(string username,string email,StreamWriter wt, StreamReader rd)
=======
        public ChangePassword(string username, string email, StreamWriter wt, StreamReader rd)
>>>>>>> 85de325 (longle)
        {
            InitializeComponent();
            this.username = username;
            this.email = email;
            reader = rd;
            writer = wt;
            panel2.Visible = false;
<<<<<<< HEAD
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_oldpass.Text))
            {
                Thread chk_password = new Thread(checkpass);
                chk_password.Start();
            }
            else {
=======


        }

        private async void btn_confirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_oldpass.Text))
            {
                try
                {
                    await CheckPasswordAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
>>>>>>> 85de325 (longle)
                label_old.Text = "Vui lòng điền mật khẩu cũ của bạn";
            }
        }

<<<<<<< HEAD
        private void btn_change_Click(object sender, EventArgs e)
=======

        private async void btn_change_Click(object sender, EventArgs e)
>>>>>>> 85de325 (longle)
        {
            if (!string.IsNullOrEmpty(tb_newpass.Text) && !string.IsNullOrEmpty(tb_confirmpass.Text))
            {
                if (tb_newpass.Text == tb_confirmpass.Text)
                {
<<<<<<< HEAD
                    Thread chg_password = new Thread(changepass);
                    chg_password.Start();
                }
                else label_new.Text = "Mật khẩu mới và xác nhận không khớp";
=======
                    await ChangePassAsync();
                }
                else
                {
                    label_new.Text = "Mật khẩu mới và xác nhận không khớp";
                }
>>>>>>> 85de325 (longle)
            }
            else
            {
                label_new.Text = "Vui lòng điền đầy đủ thông tin!";
            }
        }

        private void tb_newpass_TextChange(object sender, EventArgs e)
        {
            string password = tb_newpass.Text;

            if (password.Length < 8)
            {
                label_new.Text = "Mật khẩu cần ít nhất 8 ký tự.";
                return;
            }
            else if (!password.Any(char.IsUpper))
            {
                label_new.Text = "Mật khẩu cần ít nhất một ký tự hoa.";
                return;
            }
            else if (!password.Any(char.IsLower))
            {
                label_new.Text = "Mật khẩu cần ít nhất một ký tự thường.";
                return;
            }
            else if (!password.Any(char.IsDigit))
            {
                label_new.Text = "Mật khẩu cần ít nhất một số.";
                return;
            }
            else
            {
                // Nếu mật khẩu đáp ứng tất cả các điều kiện, xóa thông báo lỗi
                label_new.Text = "";
            }
        }
<<<<<<< HEAD

        private void checkpass()
        {
            string oldpass = tb_oldpass.Text.Trim();
            string command = $"checkpass|{email},{oldpass}";
            writer.WriteLine(command);
            string response = reader.ReadLine();
            if (!string.IsNullOrEmpty(response))
            {
                if(response == "matched")
                {
                    Invoke(new Action(() =>
                    {
                        panel2.Visible = true;
                    }));
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        label_old.Text = "Mật khẩu không chính xác! Vui lòng thử lại";
                    }));
                }
            }
        }
        private void changepass()
        {
            string newpass = tb_newpass.Text.Trim();
            string command = $"changepass|{username},{email},{newpass}";
            writer.WriteLine(command);
            string response = reader.ReadLine();
            if (!string.IsNullOrEmpty(response)) 
            {
                if (response == "success")
                {
                    MessageBox.Show("Chúc mừng bạn đã đổi mật khẩu thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra!Vui lòng thử lại sau");
                    return;
                }
            }
        }
    }
}
=======
        private async Task CheckPasswordAsync()
        {
            string oldpass = tb_oldpass.Text.Trim();
            string command = $"checkpass|{username}|{email}|{oldpass}";

            try
            {
                await Task.Run(() =>
                {
                    writer.WriteLine(command);
                    writer.Flush(); // Ensure data is sent immediately
                });

                string response = await Task.Run(() => reader.ReadLine());
                if (!string.IsNullOrEmpty(response))
                {
                    if (response == "matched")
                    {
                        panel2.Visible = true;
                        panel1.Visible = false;
                    }
                    else
                    {
                        label_old.Text = "Mật khẩu không chính xác! Vui lòng thử lại";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking password: {ex.Message}");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            LostPassword form = new LostPassword(username, email, writer, reader);
            form.ShowDialog();

        }


        private async Task ChangePassAsync()
        {
            string newpass = tb_newpass.Text.Trim();
            string command = $"changepass|{username},{email},{newpass}";

            try
            {
                await Task.Run(() =>
                {
                    writer.WriteLine(command);
                    writer.Flush(); // Ensure data is sent immediately
                });

                string response = await Task.Run(() => reader.ReadLine());

                if (!string.IsNullOrEmpty(response))
                {
                    if (response == "success")
                    {
                        MessageBox.Show("Chúc mừng bạn đã đổi mật khẩu thành công");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra! Vui lòng thử lại sau");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while changing password: {ex.Message}");
            }
        }
    }
}
>>>>>>> 85de325 (longle)
