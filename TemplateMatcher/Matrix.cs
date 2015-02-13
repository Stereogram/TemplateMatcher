using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TemplateMatcher
{

    public struct Matrix
    {
        private double[,] VALUE;

        public static implicit operator double[,](Matrix value)
        {
            return value.VALUE;
        }
        public static implicit operator Matrix(double[,] value)
        {
            Matrix t = new Matrix();
            t.VALUE = value;
            return t;
        }

        public static implicit operator Matrix(List<SPoint> value)
        {
            Matrix t = new double[value.Count,3];
            for (int i = 0; i < value.Count; i++)
            {
                t[i, 0] = value[i].X;
                t[i, 1] = value[i].Y;
                t[i, 2] = 1;
            }
            return t;
        }

        public static implicit operator List<SPoint>(Matrix value)
        {
            List<SPoint> a = new List<SPoint>();
            for (int i = 0; i < value.VALUE.GetLength(0); i++)
            {
                a.Add(new SPoint((float)value.VALUE[i,0], (float)value.VALUE[i,1]));
            }
            return a;
        }

        public double this[int i, int j]
        {
            get
            {
                return VALUE[i,j];
            }
            set
            {
                VALUE[i,j] = value;
            }
        }

        public double X
        {
            get { return VALUE[3, 0]; }
        }

        public double Y
        {
            get { return VALUE[3, 1]; }
        }

        public int LengthX
        {
            get
            {
                return VALUE.GetLength(1);
            }
        }

        public int LengthY
        {
            get
            {
                return VALUE.GetLength(0);
            }
        }

        public int GetLength(int i)
        {
            return VALUE.GetLength(i);
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix c;
            if (a.LengthX == b.LengthY)
            {
                c = new double[a.LengthY, b.LengthX];
                for (int i = 0; i < c.LengthY; i++)
                {
                    for (int j = 0; j < c.LengthX; j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < a.LengthX; k++)
                        {
                            c[i, j] = c[i, j] + a[i, k] * b[k, j];
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid Matrices");
            }
            return c;
        }

        public static Matrix Convolution(Matrix a, Matrix b)
        {
            Matrix c = new double[a.LengthX, a.LengthY];
            throw new NotImplementedException("lol");
            //return c;
        }

        public static Matrix Indentity(int n)
        {
            Matrix t = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    t[i, j] = 0.0d;
                }
                t[i, i] = 1.0d;
            }
            return t;
        }

        public static Matrix Translation(double x = 0, double y = 0)
        {
            Matrix t = Matrix.Indentity(3);
            t[2, 0] = x;
            t[2, 1] = y;
            return t;
        }

        public static Matrix Shear(double x = 0, double y = 0)
        {
            Matrix t = Matrix.Indentity(3);
            t[1, 0] = x;
            t[0, 1] = y;
            return t;
        }

        public static Matrix Scale(double x = 1, double y = 1)
        {
            Matrix t = Matrix.Indentity(3);
            t[0, 0] = x;
            t[1, 1] = y;
            return t;
        }

        public static Matrix Rotation(double d = 0.05)
        {
            Matrix t = Matrix.Indentity(3);
            t[0, 0] = Math.Cos(d);
            t[0, 1] = Math.Sin(d);
            t[1, 0] = Math.Sin(-d);
            t[1, 1] = Math.Cos(d);
            return t;
        }

        public override String ToString()
        {
            StringBuilder s = new StringBuilder("{\n");
            for (int i = 0; i < LengthY; i++)
            {
                s.Append("{");
                for (int j = 0; j < LengthX; j++)
                {
                    s.Append(VALUE[i, j]);
                    s.Append(" ");
                }
                s.Append("}");
                s.AppendLine();
            }
            s.Append("}");
            return s.ToString();
        }

    }
}
