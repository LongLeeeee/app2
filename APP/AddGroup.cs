using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace APP
{
    public partial class AddGroup : Form
    {
        string[] friendList;
        StreamWriter writer;
        List<string> userListCreateGroup;
        string username;
        ChatlistUser[] user;
        public AddGroup(string[] friendList, StreamWriter writer, string username)
        {
            InitializeComponent();
            this.friendList = friendList;
            this.writer = writer;
            userListCreateGroup = new List<string>();
            this.username = username;
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void add_click(object sender, EventArgs e)
        {
            ChatlistUser clickedChatListUser = (ChatlistUser)sender;
            if (flowLayoutPanel1.Controls.Contains(clickedChatListUser))
            {
                if (clickedChatListUser.getLabel().Visible == false)
                {
                    clickedChatListUser.getLabel().Visible = true;
                    userListCreateGroup.Add(clickedChatListUser.username);
                    clickedChatListUser.BackColor = Color.WhiteSmoke;
                    Add_Name(clickedChatListUser.username);

                }
                else
                {
                    clickedChatListUser.getLabel().Visible = false;
                    userListCreateGroup.Remove(clickedChatListUser.username);
                    clickedChatListUser.BackColor = Color.White;
                    Remove_Name(clickedChatListUser.username);
                }
            }
        }
        private void Add_Name(string name)
        {
            bunifuTextBox2.AppendText(name+"\r\n");
        }
        private void Remove_Name(string usernameToDelete)
        {

            if (bunifuTextBox2.InvokeRequired)
            {
                bunifuTextBox2.Invoke((Action)(() => Remove_Name(usernameToDelete)));
            }
            else
            {
                string textBoxContent = bunifuTextBox2.Text;

                string[] lines = textBoxContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                List<string> updatedLines = new List<string>();

                foreach (string line in lines)
                {
                    // Chỉ thêm các dòng không chứa chính xác username cần xóa
                    if (!line.Equals(usernameToDelete, StringComparison.Ordinal))
                    {
                        updatedLines.Add(line);
                    }
                }

                string updatedContent = string.Join("\r\n", updatedLines);
                bunifuTextBox2.Text = updatedContent;
            }
        }
        private void AddGroup_Load(object sender, EventArgs e)
        {
            if (friendList != null)
            {
                user = new ChatlistUser[friendList.Length];
                for (int i = 0; i < friendList.Length; i++)
                {
                    if (friendList[i] != "")
                    {
                        user[i] = new ChatlistUser();
                        Image image = Image.FromFile("Resources\\avata.jpg");
                        // tạo ra 1 chatlistuser
                        user[i].username = friendList[i];
                        user[i].userimage = image;
                        user[i].MouseDown += add_click;
                        flowLayoutPanel1.Controls.Add(user[i]);
                    }
                }
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bunifuTextBox1.Text) && userListCreateGroup != null)
            {
                string userListCreateGroupString = "";
                foreach (var item in userListCreateGroup)
                {
                    userListCreateGroupString += item + "|";
                }
                userListCreateGroupString += username + "|";
                writer.WriteLine("CreateGroup");
                writer.WriteLine(username);
                writer.WriteLine(userListCreateGroupString);
                writer.WriteLine(bunifuTextBox1.Text);
                this.Close();
            }
            else
            {

            }
        }
       
    }
}
