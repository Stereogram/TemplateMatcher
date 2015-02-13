using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace TemplateMatcher
{
    public class Vector
    {
        public double Magnitude  {get; set;}
        public SPoint Direction  {get; set;}

        public static implicit operator SPoint(Vector value)
        {
            return value.Direction;
        }
        public static implicit operator Vector(Point value)
        {
            Vector t = new Vector(value);
            return t;
        }

        public Vector(Vector a)
        {
            this.Direction = a.Direction;
            this.Magnitude = a.Magnitude;
        }

        public Vector(SPoint start, SPoint end)
        {
            Magnitude = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            Direction = start - end;
        }

        public Vector(SPoint direction)
        {
            Direction = direction;
            Magnitude = Math.Sqrt(Math.Pow(Direction.X, 2) + Math.Pow(Direction.Y, 2));
        }

        public Vector(double m, SPoint d)
        {
            Magnitude = m;
            Direction = d;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.Direction + b.Direction);
        }

        public static Vector operator +(Vector a, SPoint b)
        {
            return new Vector(a.Direction + b);
        }

        public static Vector operator *(Vector a, double b)
        {
            return new Vector(a.Magnitude * b, a.Direction);
        }

        public static Vector operator /(Vector a, double b)
        {
            return new Vector(a.Magnitude / b, a.Direction);
        }

        public static Vector operator -(Vector a, Vector b)
        {

            return new Vector(a.Direction - b.Direction);
        }

        public double Proj(Vector b)
        {
            return Projection(this, b);
        }

        public double Dot(Vector b)
        {
            return DotProduct(this, b);
        }

        public static double Projection(Vector a, Vector b)
        {
            return DotProduct(a,b) / a.Magnitude;
        }

        public static double DotProduct(Vector a, Vector b)
        {
            return a.Direction.X * b.Direction.X + a.Direction.Y * b.Direction.Y;
        }

        public override String ToString()
        {
            return Magnitude + " " + Direction;
        }
    }
}
