using ParkWalk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWalk.Engines
{
    internal class GraphGenerator
    {
        public static Graph Generate(int NodeCount)
        {
            Graph output = new Graph(NodeCount);
            Random random = new Random();
            int randWeights;
            // Create Hamilton Loop
            uint[] ids = output.nodes.Select(node => node.Id).ToArray();
            // Shuffle the array
            Random rand = new Random();
            for(int i = 0; i < 3 * ids.Length; i++)
            {
                int idx1 = rand.Next(1, ids.Length - 1);
                int idx2 = rand.Next(1, ids.Length - 1);

                //swap two elements
                uint temp = ids[idx1];
                ids[idx1] = ids[idx2];
                ids[idx2] = temp;
            }

            // Create Paths
            for (int i = 0; i < ids.Length - 1; i++)
            {
                Node firstNode = output.nodes.FirstOrDefault(node => node.Id == ids[i]);
                Node secondNode = output.nodes.FirstOrDefault(node => node.Id == ids[i + 1]);
                randWeights = random.Next(1, 10);
                output.AddPath(firstNode.Id, secondNode.Id, randWeights);
            }

            // Generage Random Additional Paths
            for (int i = 0; i < output.nodes.Count; i++)
            {
                int maxEdges = rand.Next(1, NodeCount / 4 + 2);
                for (int j = 1; j < maxEdges; j++)
                {
                    List<Node> notConnected = output.nodes
                    .Where(node =>
                        node.Paths.Any(path => path.To == output.nodes[i]) == false && node.Equals(output.nodes[i]) == false)
                    .ToList();
                    if (notConnected.Count == 0) break;
                    Node connectTo = notConnected[rand.Next(notConnected.Count)];
                    randWeights = random.Next(1, 10);
                    output.AddPath(output.nodes[i].Id, connectTo.Id, randWeights);

                }
            }
            // Generate random weights for all paths.
            return output;
        }
    }
}
