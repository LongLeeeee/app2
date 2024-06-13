using Bunifu.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private Image _userimage;
        public BunifuLabel getLabel()
        {
            return bunifuLabel3;
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
                Name.Text = value;
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
    }
}
