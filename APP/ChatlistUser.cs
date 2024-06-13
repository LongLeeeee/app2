using Bunifu.UI.WinForms;
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

namespace APP
{
    public partial class ChatlistUser : UserControl
    {
        public ChatlistUser()
        {
            InitializeComponent();
        }
        private string _username;
        private string _name1;
        private Image _userimage;
        public BunifuLabel getLabel()
        {
            return bunifuLabel1;
        }
        public string name1
        {
            get
            {
                return _name1;
            }
            set
            {
                _name1 = value;
                name.Text = value;
            }
        }
        public string username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                usrn.Text = value;
            }
        }
        public Image userimage
        {
            get
            {
                return _userimage;
            }
            set
            {
                _userimage = value;
                Image.Image = value;
            }
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
