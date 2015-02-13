using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TemplateMatcher;

namespace TemplateMatcher
{
    public class Shape
    {
        public List<Vector> RTable {get; private set;}
        public Bitmap ShapeBitmap {get; private set;}
        public string Name {get; set;}
        public List<SPoint> ShapePoints {get; set;}
        public SPoint Center {get; private set;}
        public Rectangle HitBox {get; private set;}
        private ShapeParser t;

        public Shape(Bitmap a, String name) : this(a)
        {
            Name = name;
        }

        public Shape(Bitmap a)
        {
            RTable = new List<Vector>();
            t = new ShapeParser(a);
            t.BuildBoundingBox();
            ShapePoints = t.ShapeArray;
            this.ShapeBitmap = t.Image;
            Centerize();
        }

        public void ResetPoints()
        {
            t.ShapeArray = ShapePoints;
            t.BuildBoundingBox();
            SPoint sub = new SPoint(0, t.bounds.Top);
            for (int i = 0; i < ShapePoints.Count(); i++ )
            {
                ShapePoints[i] -= sub;
            }
            //ShapePoints.ForEach(delegate(SPoint s) { s -= new SPoint(100,100); });
        }

        
        private void Centerize()
        {
            Center = new SPoint(ShapeBitmap.Width / 2, ShapeBitmap.Height / 2);
            HitBox = new Rectangle((int)Center.X - 1, (int)Center.Y - 1, 3, 3);
            using (Graphics g = Graphics.FromImage(ShapeBitmap))
            {
                g.DrawRectangle(Pens.Red, Center.X, Center.Y, 1, 1);
            }
        }

        public void FillTable()
        {
            foreach (SPoint p in ShapePoints)
            {
                RTable.Add(new Vector(Center, p));
            }
        }

    }
}
