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
    public partial class SeFile : UserControl
    {
        public SeFile()
        {
            InitializeComponent();
        }
        private string _filename;
        public string filename
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
                bunifuLabel1.Text = value;
            }
        }
    }
}
