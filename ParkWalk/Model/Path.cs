using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ParkWalk.Model
{
    internal class Path : IDrawable
    {
        public Node From { get; set; }
        public Node To { get; set; }
        private int weight;
        public int Weight { 
            get
            {
                return weight;
            }
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException();
                }

                weight = value;
            }
        }

        Color[] ColorLegend = {
            Color.LightGray,
            Color.Black,
            Color.White,
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Magenta,
            Color.Yellow,
            Color.Turquoise,
            Color.Cyan,
            Color.Lime,
        };

        private Color WeightToColor()
        {
            return ColorLegend[Weight];
        }

        public void Draw(Graphics grfx)
        {
            Point[] points = GenStartEndPoints();
            grfx.DrawLine(new Pen(WeightToColor(), 3), points[0], points[1]);
        }

        enum RelatedPosition
        {
            Up,
            Right,
            Down,
            Left
        }

        private RelatedPosition GetRelatedPosition(int deltax, int deltay)
        {
            float x = (float)deltax;
            float y = (float)deltay;

            if (Math.Abs(x / y) >= 1 && x >= 0)
            {
                return RelatedPosition.Right;
            } 
            else if (Math.Abs(x / y) < 1 && y < 0) 
            {
                return RelatedPosition.Down;
            }
            else if (Math.Abs(x / y) >= 1 && x < 0)
            {
                return RelatedPosition.Left;
            }
            else
            {
                return RelatedPosition.Up;
            }
        }
        private Point[] GenStartEndPoints()
        {
            Point p1 = new Point(From.Location.X + Node.Size.Width / 2, From.Location.Y + Node.Size.Height / 2);
            Point p2 = new Point(To.Location.X + Node.Size.Width / 2, To.Location.Y + Node.Size.Height / 2);

            // Calculate angle between centres in Decart coordinate system
            int deltax = p2.X - p1.X;
            int deltay = p1.Y - p2.Y;

            switch(GetRelatedPosition(deltax, deltay))
            {
                case RelatedPosition.Right:
                    p1.X += Node.Size.Width / 2;
                    p2.X -= Node.Size.Width / 2;
                    break;
                case RelatedPosition.Down:
                    p1.Y += Node.Size.Height / 2;
                    p2.Y -= Node.Size.Height / 2; 
                    break;
                case RelatedPosition.Left:
                    p1.X -= Node.Size.Width / 2;
                    p2.X += Node.Size.Width / 2;
                    break;
                case RelatedPosition.Up:
                    p1.Y -= Node.Size.Height / 2;
                    p2.Y += Node.Size.Height / 2;
                    break;
            }

            return new Point[] { p1, p2 };
        }
    }
}
