using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkWalk.Model
{
    internal interface IDrawable
    {
        void Draw(Graphics grfx);
    }
}
