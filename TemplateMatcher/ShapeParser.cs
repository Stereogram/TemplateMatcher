using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TemplateMatcher
{
    public class ShapeParser
    {
        public List<SPoint> ShapeArray {get; set;}
        public Bitmap Image{get; private set;}
        private Func<Color, bool> Compare = a => !a.Name.Equals("ff000000");
        public RectangleF bounds{get; private set;}

        public ShapeParser(Bitmap a)
        {
            Image = a;
            ShapeArray = new List<SPoint>();
            BuildArray();
            BuildBoundingBox();
            Image = CropImage(Image, bounds);
            BuildArray();

        }

        public void BuildArray()
        {
            ShapeArray.Clear();
            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    if (Compare(Image.GetPixel(i, j)) )
                    {
                        ShapeArray.Add(new SPoint(i, j));
                        //FollowShape(new SPoint(i, j));
                        //break;
                    }
                }
            }
        }

        public void BuildBoundingBox()
        {
            SPoint minx = new SPoint(9999, 9999), miny = new SPoint(9999, 9999), maxx = new SPoint(0, 0), maxy = new SPoint(0, 0);
            foreach (SPoint p in ShapeArray)
            {
                minx = p.X < minx.X ? p : minx;
                miny = p.Y < miny.Y ? p : miny;
                maxx = p.X > maxx.X ? p : maxx;
                maxy = p.Y > maxy.Y ? p : maxy;
            }
            bounds = new RectangleF(minx.X, miny.Y, maxx.X-minx.X+1, maxy.Y-miny.Y+1);
        }

        private Bitmap CropImage(Bitmap bmpImage, RectangleF cropArea)
        {
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        //private void FollowShape(SPoint p)
        //{
        //    SPoint end = new SPoint(0,0);
        //    SPoint t;
        //    ShapeArray.Add(p);
        //    while ( !(t = FindNext(p)).Equals(end) )
        //    {
        //        ShapeArray.Add(t);
        //        img.SetPixel(p, Color.Black);
        //        p = t;
        //    }
        //}

        //private SPoint FindNext(SPoint p)
        //{
        //    SPoint a, p1,p2,p3,p4,p5,p6,p7,p8;
        //    p1 = p + new Point(1, 0);//right
        //    p2 = p + new Point(0, 1);//up
        //    p3 = p + new Point(0,-1);//down
        //    p4 = p + new Point(1, 1);//upright
        //    p5 = p + new Point(1,-1);//downright
        //    p6 = p + new Point(-1,0);//left
        //    p7 = p + new Point(-1,-1);//downleft
        //    p8 = p + new Point(-1,1);//upleft
        //    if (Compare(img.GetPixel(p1)))      a = p1;
        //    else if (Compare(img.GetPixel(p2))) a = p2;
        //    else if (Compare(img.GetPixel(p3))) a = p3;
        //    else if (Compare(img.GetPixel(p4))) a = p4;
        //    else if (Compare(img.GetPixel(p5))) a = p5;
        //    else if (Compare(img.GetPixel(p6))) a = p6;
        //    else if (Compare(img.GetPixel(p7))) a = p7;
        //    else if (Compare(img.GetPixel(p8))) a = p8;
        //    else a = new SPoint(0, 0);
        //    return a;
        //}

    }
}
