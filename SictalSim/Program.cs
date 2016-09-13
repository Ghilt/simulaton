using SictalSim.Attributes;
using SictalSim.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine engine = new Engine();
            Region r = new Region(0, 100, 100);
            Human h = new Human(0, new Location(r, 10, 10));
            engine.AddSimulator(r);
            engine.AddSimulator(h);
            engine.Start();
        }
    }
}
