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
    public partial class ReIcon : UserControl
    {
        public ReIcon()
        {
            InitializeComponent();
        }
        private Image _image;
        public Image image
        {
            get {
                return _image;
                 }
            set
            {
                _image = value;
                bunifuPictureBox1.Image = value;
            }
        }
    }
}
