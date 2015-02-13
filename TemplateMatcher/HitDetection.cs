using System;
using System.Collections.Generic;
using System.Linq;
using TemplateMatcher;

namespace TemplateMatcher
{
    public class HitDetection
    {
        public List<SPoint> rotationpoints;

        public List<float> Percent {get; private set;}
        public List<string> Results {get; private set;}
        public List<string> Scale {get; private set;}
        public List<string> Rotation {get; private set;}
        private Shape a;
        private Library b;

        public HitDetection(Shape a, Library b)
        {
            this.a = a;
            this.b = b;
            Percent = new List<float>();
            Results = new List<string>();
            Scale = new List<string>();
            Rotation = new List<string>();
        }

        public void Start()
        {
            float percent = 0, cpercent = 0.0f;
            double scalex, scaley;
            int c, crot = 0;
            //Shape temp;
            Matrix points;
            Matrix ctrans,m,m2,r;
            SPoint center;
            foreach(Shape s in b)
            {
                points = a.ShapePoints;
                ctrans = Matrix.Indentity(3);
                //temp = a;
                scalex = a.ShapeBitmap.Width < s.ShapeBitmap.Width ? Math.Round(s.Center.X / a.Center.X) : 1/Math.Round(a.Center.X / s.Center.X);
                scaley = a.ShapeBitmap.Height < s.ShapeBitmap.Height ? Math.Round(s.Center.X / a.Center.X) : 1/Math.Round(a.Center.X / s.Center.X);
                ctrans *= Matrix.Scale(scalex, scaley);
                center = a.Center;
                center.X *= (float)scalex;
                center.Y *= (float)scaley;
                points *= ctrans;
                /*crot = 0;-----------------BROKEN ROTATION STUFF
                for (int i = 0; i < 8; i++)
                {
                    //points = a.ShapePoints;
                    
                    m = Matrix.Translation(-center.X, -center.Y);
                    r = Matrix.Rotation(Math.PI/4);
                    m2 = Matrix.Translation(center.X, center.Y);
                    ctrans *= (m * r * m2);

                    temp.ShapePoints *= ctrans;

                    temp.ResetPoints();

                    //points *= ctrans;

                    c = Hit(s, temp.ShapePoints);
                    percent = ((float)c / (float)temp.ShapePoints.Count()) * 100.0f;

                    if (percent > cpercent)
                    {
                        cpercent = percent;
                        crot = i;
                        if (percent == 100.0f)
                        {
                            break;
                        }

                    }
                }
                crot *= 45;*/
                c = Hit(s, points);
                percent = ((float)c / (float)a.ShapePoints.Count()) * 100.0f;
                Percent.Add(percent);
                Results.Add(s.Name);
                Scale.Add( scalex.ToString("0.0")+","+scaley.ToString("0.0") );
                //Rotation.Add(crot.ToString());

            }
        }

        private int Hit(Shape s, List<SPoint> b)
        {
            int r = 0;
            foreach(SPoint p in b)
            {
                foreach(Vector v in s.RTable)
                {
                    if (s.HitBox.Contains((SPoint)(p + v)))
                    {
                        r++;
                        break;
                    }
                }
            }
            return r;
        }

    }
}
