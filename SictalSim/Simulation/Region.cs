using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Simulation
{

    // interesting resource http://www.redblobgames.com/grids/hexagons/
    class Region : Simulator
    {
        private int width;
        private int length;

        public Region(int ticksBirth, int width, int length) : base(ticksBirth)
        {
            this.width = width;
            this.length = length;
        }

        internal int GetWidth()
        {
            return width;
        }

        internal int GetLength()
        {
            return length;
        }

        public override void PerformTick()
        {
           //do nothing yet
        }

        public override string GetCurrentInfoLog()
        {
            return "Region, width: " + width + " Length: " + length;
        }
    }
}
