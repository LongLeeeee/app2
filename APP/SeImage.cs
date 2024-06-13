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
    public partial class SeImage : UserControl
    {
        public SeImage()
        {
            InitializeComponent();
        }
        private Image _image;
        public Image image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                pictureBox1.Image = value;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog.FileName);
                MessageBox.Show("Ảnh đã được tải xuống thành công!");
            }
        }
    }
}
