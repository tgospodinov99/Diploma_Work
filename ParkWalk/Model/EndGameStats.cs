using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWalk.Model
{
    internal class EndGameStats
    {
        public List<int> Paths { get; set; } = new List<int>();
        public string Message { get; set; }
    }
}
