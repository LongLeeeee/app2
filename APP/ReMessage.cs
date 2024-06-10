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
    public partial class ReMessage : UserControl
    {
        public ReMessage()
        {
            InitializeComponent();
        }
        private string _messgae;
        public string messgae
        {
            get
            {
                return _messgae;
            }
            set
            {
                _messgae = value;
                bunifuLabel1.Text = value;
                
            }
        }
    }
}
