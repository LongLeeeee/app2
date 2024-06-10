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

namespace APP.Resources
{
    public partial class ChangeNameAndImae : Form
    {
        string username, email,name;
        StreamWriter writer;
        StreamReader reader;

        private void ChangeNameAndImae_Load(object sender, EventArgs e)
        {
            bunifuLabel1.Text = name;
        }

        public ChangeNameAndImae(string username, string email,string name, StreamWriter wt,StreamReader rd)
        {
            InitializeComponent();
            this.username = username;
            this.email = email;
            reader = rd;
            writer = wt;
            this.name = name;
            
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            string newname = tb_newname.Text.Trim();
            writer.WriteLine($"changename|{username}|{newname}");
            string response = reader.ReadLine();
            if (response != null)
            {
                if (response == "changename_success")
                {
                    MessageBox.Show("Đổi tên thành công");
                    tb_newname.Clear();
                    bunifuLabel1.Text = newname;
                }
            }
        }
    }
}
