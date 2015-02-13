using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TemplateMatcher
{
    public static class Extensions
    {

        public static Color GetPixel(this Bitmap a, Point p)
        {
            return a.GetPixel(p.X, p.Y);
        }

        public static void SetPixel(this Bitmap a, Point p, Color c)
        {
            a.SetPixel(p.X, p.Y,c);
        }

    }
}
