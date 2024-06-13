using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
<<<<<<< HEAD
=======
using System.IO;
>>>>>>> 85de325 (longle)
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP.Resources
{
    public partial class ChangeNameAndImae : Form
    {
<<<<<<< HEAD
        public ChangeNameAndImae()
        {
            InitializeComponent();
=======
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
            this.Close();
>>>>>>> 85de325 (longle)
        }
    }
}
