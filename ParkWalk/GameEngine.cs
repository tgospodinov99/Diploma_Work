using ParkWalk.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParkWalk
{
    internal class GameEngine
    {
        public Player player { get; }
        public List<uint> Log = new List<uint>();
        
        public GameEngine(Player Player)
        {
            this.player = Player;
            Log.Add(0);
        }
        private void MovePlayer(int From, int To)
        {
            if (player.CurrentNode.Id != From)
            {
                throw new ArgumentException($"Player is not on node number {From}.");
            }

            Path DestinationPath = player.CurrentNode.Paths.FirstOrDefault(path => path.To.Id == To);
            if (DestinationPath == null)
            {
                throw new ArgumentException($"There is no path from node number {From} to node number {To}.");
            }

            player.Move(DestinationPath.To);
            Log.Add(DestinationPath.To.Id);
        }
        public void ExecuteCommand(string Command)
        {
            Command = Command.ToLower().Replace(" ", "");
            if (Regex.IsMatch(Command, @"^move\([0-9]+,[0-9]+\);$"))
            {
                
                string numbers = Regex.Match(Command, "[0-9]+,[0-9]+").Value;
                int[] nodes = numbers
                    .Split(',')
                    .Select(int.Parse)
                    .ToArray();

                MovePlayer(nodes[0], nodes[1]);
            }
        }

        public bool CheckWinner()
        {
            return player.CurrentNode.Id == player.gameArena.nodes.Max(n => n.Id);
        }

        public void Draw(Graphics grfx)
        {
            player.gameArena.Draw(grfx);
        }


        private EndGameStats GetPathLogInfo()
        {
            EndGameStats stats = new EndGameStats();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < Log.Count - 1; i++)
            {
                int pathLength = player.gameArena
                    .nodes
                    .FirstOrDefault(n => n.Id == Log[i])
                    .Paths
                    .FirstOrDefault(p => p.To.Id == Log[i + 1]).Weight;
                stats.Paths.Add(pathLength);
                sb.AppendLine($"From node {Log[i]} to node {Log[i + 1]}, lenght = {pathLength}");
            }
            stats.Message = sb.ToString();
            return stats;
        }

        public string ShowEndgameStats()
        {
            return $"You win! It took you {Log.Count - 1} moves. \n{GetPathLogInfo().Message}\nSum of weights = {GetPathLogInfo().Paths.Sum()}";
        }
    }
}
