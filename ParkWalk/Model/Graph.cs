using ParkWalk.Engines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWalk.Model
{
    internal class Graph
    {
        readonly Rectangle PrintBoard = new Rectangle(161, 252, 287, 287);
        public List<Node> nodes { get; } = new List<Node>();



        public Graph(int count)
        {
            GenNodes(count);
        }

        private void GenNodes(int count)
        {
            nodes.Add(new Node(0, new System.Drawing.Point(PrintBoard.X, PrintBoard.Y)));
            Random random = new Random();
            for (uint i = 1; i < count; i++)
            {
                Rectangle rect;
                int x;
                int y;
                do
                {
                    x = random.Next(0, 5);
                    y = random.Next(0, 5);
                    Point point = new Point(PrintBoard.X + 10 + PrintBoard.Width / 5 * x,
                                            PrintBoard.Y + 10 + PrintBoard.Height / 5 * y);
                    rect = new Rectangle(point, Node.Size);
                } while (nodes.Any(node => CheckOverlay(node.Location, rect)) || x == 0 && y == 0);
                nodes.Add(new Node(i, rect.Location));
            }

            // Get points out of their boxes
            for (int i = 1; i < nodes.Count; i++)
            {
                int randomx = random.Next(-10, 10);
                int randomy = random.Next(-10, 10);
                nodes[i].Location = new Rectangle(
                    nodes[i].Location.X + randomx,
                    nodes[i].Location.Y + randomy,
                    nodes[i].Location.Width,
                    nodes[i].Location.Height
                    );
            }
        }

        private bool CheckOverlay(Rectangle rect1, Rectangle rect2)
        {
            return (rect1.X == rect2.X && rect1.Y == rect2.Y);
        }

        public void AddPath(uint Id1, uint Id2, int weight)
        {
            if (Id1 >= nodes.Count || Id2 >= nodes.Count || Id1 == Id2)
            {
                throw new ArgumentException();
            }

            

            nodes[(int)Id1].Paths.Add(new Path()
            {
                From = nodes[(int)Id1],
                To = nodes[(int)Id2],
                Weight = weight
            });

            nodes[(int)Id2].Paths.Add(new Path()
            {
                From = nodes[(int)Id2],
                To = nodes[(int)Id1],
                Weight = weight
            });

            
            
        }

        public void Draw(Graphics grfx)
        {
            foreach(Node node in nodes)
            {
                node.Draw(grfx);
                foreach (Path path in node.Paths)
                {
                    path.Draw(grfx);
                }
            }       
        }
       
        public int[] GetShortestPath()
        {
            DijkstrasAlgorithm dijkstra = new DijkstrasAlgorithm(GetAdjMatrix());
            return dijkstra.GetShortestPath(0, nodes.Count - 1).ToArray();
        }
        
        
        private int[,] GetAdjMatrix()
        {
            int[,] matrix = new int[nodes.Count, nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                Node rowNode = nodes.FirstOrDefault(node => node.Id == i);
                for (int j = 0; j < nodes.Count; j++)
                {
                    Path destinationPath = rowNode.Paths.FirstOrDefault(path => path.To.Id == j);
                    if (destinationPath != null)
                    {
                        matrix[i, j] = destinationPath.Weight;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }

            return matrix;
        }
    }
}
