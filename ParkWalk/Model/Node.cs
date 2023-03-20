using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWalk.Model
{
    enum NodeState
    {
        Unvisited,
        Visited,
        Current
    }
    internal class Node : IDrawable
    {
        public static readonly Size Size = new Size(30 , 30);
        public Rectangle Location { get; set; }
        public List<Path> Paths { get; } = new List<Path>();
        public uint Id { get; set; }
        public NodeState State { get; set; }
        public Node(uint Id)
        {
            this.Id = Id;
        }

        public Node(uint Id, Point Location)
            : this(Id)
        {
            this.Location = new Rectangle(Location, Size);
            State = Id == 0 ? NodeState.Current : NodeState.Unvisited;
        }

        private Pen GenPen()
        {
            Color color;
            switch (State)
            {
                case NodeState.Unvisited:
                    color = Color.Black;
                    break;
                case NodeState.Visited:
                    color = Color.Green;
                    break;
                case NodeState.Current:
                    color = Color.Red;
                    break;
                default:
                    color = Color.Black;
                    break;
            }

            return new Pen(color, 4);
        }

        public void Draw(Graphics grfx)
        {
            // Draw rectangle
            grfx.DrawRectangle(GenPen(), Location);

            // Draw text
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            grfx.DrawString(Id.ToString(), new Font(FontFamily.GenericSerif, 20), Brushes.Black, Location, sf);
        }
    }
}
