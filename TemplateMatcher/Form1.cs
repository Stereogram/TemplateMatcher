using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TemplateMatcher;

namespace TemplateMatcher
{
    public partial class Form1 : Form
    {
        private Bitmap img;
        private Bitmap library;
        private Library shapes;
        private Shape t;

        private int cur = 0;

        public Form1()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            shapes = new Library();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(new CancelEventArgs(true));
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "All Picture Files|*.bmp;*.jpg;*.jpeg;*.png|Bitmap|*.bmp|JPEG|*.jpg;*.jpeg|PNG|*.png";
            open.Title = "Open a shape!";
            if (open.ShowDialog() == DialogResult.OK)
            {
                img = new Bitmap(open.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                panel1.AutoScrollMinSize = img.Size;
                //pictureBox1.Image = img;
                t = new Shape(img,"test shape");

                //using (Graphics g = Graphics.FromImage(t.ShapeBitmap))
                //{
                //    foreach (Vector p in t.RTable)
                //    {
                //        g.DrawRectangle(Pens.HotPink, p.Direction.X, p.Direction.Y, 1, 1);
                //    }
                //}
                
                pictureBox1.Image = t.ShapeBitmap;
            }
        }

        private void loadLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "All Picture Files|*.bmp;*.jpg;*.jpeg;*.png|Bitmap|*.bmp|JPEG|*.jpg;*.jpeg|PNG|*.png";
            open.Title = "Open the library!";
            if (open.ShowDialog() == DialogResult.OK)
            {
                library = new Bitmap(open.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
                panel2.AutoScrollMinSize = library.Size;
                pictureBox2.Image = library;
                shapes.Add(library);
                UpdateLibraryText();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            cur = 0;
        }

        private void UpdateLibraryText()
        {
            pictureBox3.Image = shapes[cur].ShapeBitmap;
            pictureBox3.Refresh();
            numberBox.Text = cur.ToString();
            shapeNameBox.Text = shapes[cur].Name;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (++cur >= shapes.Length)
            {
                cur = 0;
            }
            UpdateLibraryText();
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (--cur < 0)
            {
                cur = shapes.Length-1;
            }
            UpdateLibraryText();
        }

        private void numberBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                int ret;
                if (int.TryParse(numberBox.Text, out ret))
                {
                    if (ret < shapes.Length && ret >= 0)
                    {
                        cur = ret;
                        UpdateLibraryText();
                    }
                }
            }
        }

        private void shapeNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                shapes[cur].Name = shapeNameBox.Text;
            }
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            HitDetection a = new HitDetection(t, shapes);
            a.Start();
            //timer1.Start();
            //using (Graphics g = Graphics.FromImage(t.ShapeBitmap))
            //{
            //    foreach (SPoint p in a.rotationpoints)
            //    {
            //        g.DrawRectangle(Pens.Red, p.X, p.Y, 1, 1);
            //    }
            //}
            //pictureBox1.Image = t.ShapeBitmap;

            resultBox.Text += Environment.NewLine;
            for (int i = 0; i < a.Results.Count; i++)
            {
                resultBox.Text += "Shape: " + a.Results[i];
                resultBox.Text += " Scale: " + a.Scale[i];
                //resultBox.Text += " Rotation: " + a.Rotation[i] + " degrees";
                resultBox.Text += " Percent: " + a.Percent[i].ToString("0.00") + "% ";
                resultBox.Text += Environment.NewLine;
            }


            



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Matrix r = Matrix.Rotation(Math.PI / 180);
            Matrix m = Matrix.Translation(-t.Center.X, -t.Center.Y);
            Matrix m2 = Matrix.Translation(t.Center.X, t.Center.Y);
            Matrix ctrans = Matrix.Indentity(3);
            ctrans = (m * r * m2);
            t.ShapePoints *= ctrans;
            t.ResetPoints();
            Bitmap test = new Bitmap(t.ShapeBitmap.Width, t.ShapeBitmap.Height);
            using (Graphics g = Graphics.FromImage(test))
            {
                foreach (SPoint p in t.ShapePoints)
                {
                    g.DrawRectangle(Pens.Red, p.X, p.Y, 1, 1);
                }
            }
            pictureBox1.Image = test;
            this.Invalidate();
        }


    }
}
