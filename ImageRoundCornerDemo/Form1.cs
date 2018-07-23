using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageRoundCornerDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image image = Image.FromFile("C:\\nocorner.png");
            Image corner = this.RoundCorners(image, 15, Color.Transparent);
            this.pictureBox1.Image = image;
            this.pictureBox2.Image = corner;
        }

        public Image RoundCorners(Image StartImage, int CornerRadius, Color BackgroundColor)
        {
            CornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);

            using (Graphics g = Graphics.FromImage(RoundedImage))
            {
                g.Clear(BackgroundColor);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                using (Brush brush = new TextureBrush(StartImage))
                {
                    using (GraphicsPath gp = new GraphicsPath())
                    {
                        gp.AddArc(-1, -1, CornerRadius, CornerRadius, 180, 90);
                        gp.AddArc(0 + RoundedImage.Width - CornerRadius, -1, CornerRadius, CornerRadius, 270, 90);
                        gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                        gp.AddArc(-1, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);

                        g.FillPath(brush, gp);
                    }
                }

                return RoundedImage;
            }
        }
    }
}
