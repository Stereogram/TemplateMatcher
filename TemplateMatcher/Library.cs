using System.Collections.Generic;
using System.Drawing;

namespace TemplateMatcher
{
    public class Library
    {
        private readonly List<Shape> shapes;
        private int count;

        public Library()
        {
            shapes = new List<Shape>();
        }

        public void Add(Bitmap a)
        {
            Shape b = new Shape(a, "New Shape " + count++);
            b.FillTable();
            shapes.Add(b);
        }

        public Shape this[int i]
        {
            get{return shapes[i];}
        }

        public void Clear()
        {
            shapes.Clear();
        }

        public IEnumerator<Shape> GetEnumerator()
        {
            return shapes.GetEnumerator();
        }

        public int Length { get { return shapes.Count; } } 

    }
}
