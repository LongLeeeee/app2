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
    public partial class SeMessage : UserControl
    {
        public SeMessage()
        {
            InitializeComponent();
        }
        private string _message;
        public string message
        {
            get { return _message; }
            set
            {
                _message = value;
                bunifuLabel1.Text = value;
            }
        }
    }
}
