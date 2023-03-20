using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWalk.Model
{
    internal class Player
    {
        public Graph gameArena { get; }
        public Node CurrentNode { get; set; }
        public Player(Graph GameArena)
        {
            this.gameArena = GameArena; 
            this.CurrentNode = GameArena.nodes.First();
        }

        public void Move(Node DestinationNode)
        {
            Path destinationPath = CurrentNode.Paths.FirstOrDefault(path => path.To.Equals(DestinationNode));
            DestinationNode = destinationPath.To;
            if (DestinationNode == null)
            {
                throw new ArgumentException("Destination not linked to current node.");
            }

            CurrentNode.State = NodeState.Visited;
            CurrentNode = DestinationNode;
            CurrentNode.State = NodeState.Current;
            
        }
    }
}
