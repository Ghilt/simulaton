using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{

    // interesting resource http://www.redblobgames.com/grids/hexagons/
    class Region : Simulator
    {
        private int width;
        private int length;
        private Interval resourcesAvailable;

        public Region(int ticksBirth, int width, int length) : base(ticksBirth)
        {
            this.width = width;
            this.length = length;
            resourcesAvailable = new Interval(0.1f, 0.9f);
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

        internal float Extract(int x, int y, int needId)
        {
            //TODO
            return resourcesAvailable.NextFloat();
        }

        public override string GetCurrentInfoLog()
        {
            return "Region, width: " + width + " Length: " + length;
        }

        public override void OnTerminate()
        {
            throw new NotImplementedException();
        }
    }
}
