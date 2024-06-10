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
    public partial class ReImage : UserControl
    {
        public ReImage()
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
            if (_image != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                    saveFileDialog.Title = "Save an Image File";
                    saveFileDialog.FileName = "Image";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            string filePath = saveFileDialog.FileName;
                            string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();
                            System.Drawing.Imaging.ImageFormat format;

                            switch (fileExtension)
                            {
                                case ".jpg":
                                    format = System.Drawing.Imaging.ImageFormat.Jpeg;
                                    break;
                                case ".bmp":
                                    format = System.Drawing.Imaging.ImageFormat.Bmp;
                                    break;
                                case ".png":
                                    format = System.Drawing.Imaging.ImageFormat.Png;
                                    break;
                                default:
                                    throw new InvalidOperationException("Unsupported file format.");
                            }

                            
                            using (var bmp = new Bitmap(_image))
                            {
                                bmp.Save(filePath, format);
                            }

                            //MessageBox.Show($"Image saved successfully at {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while saving the image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No image available to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
