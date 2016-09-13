using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Simulation
{
    class Human : Simulator
    {

        private Location location;

        public Human(int ticksBirth, Location location) : base(ticksBirth)
        {
            this.location = location;
        }


        public override void PerformTick()
        {
            location.Move();
        }

        public override string GetCurrentInfoLog()
        {
            return "Human, at x: " + location.GetX() + " y: " + location.GetY(); ;
        }
    }
}
