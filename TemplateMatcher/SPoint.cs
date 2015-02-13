using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TemplateMatcher
{
    public struct SPoint
    {
        public float X{get;set;} 
        public float Y{get;set;}

        public SPoint(float x, float y): this()
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator Point(SPoint value)
        {
            return new Point((int)value.X,(int)value.Y);
        }
        public static implicit operator SPoint(Point value)
        {
            return new SPoint(value.X, value.Y);
        }

        public static SPoint operator +(SPoint p, SPoint b)
        {
            return new SPoint(p.X + b.X, p.Y + b.Y);
        }

        public static SPoint operator -(SPoint p, SPoint b)
        {
            return new SPoint(p.X - b.X, p.Y - b.Y);
        }

        public static bool operator <(SPoint a, SPoint b)
        {
            return a.X < b.X && a.Y < b.Y ? true : false;
        }

        public static bool operator >(SPoint a, SPoint b)
        {
            return a.X > b.X && a.Y > b.Y ? true : false;
        }

        public override String ToString()
        {
            return this.X + " " + this.Y;
        }

    }
}
