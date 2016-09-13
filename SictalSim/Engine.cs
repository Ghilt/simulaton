using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SictalSim.Simulation;

namespace SictalSim
{
    class Engine
    {
        private int tick;
        private List<Simulator> entities;

        public Engine()
        {
            tick = 0;
            entities = new List<Simulator>();
        }

        public void Start()
        {
            Logger.printInfo("Started");
            while (true)
            {
                tick++;
                foreach (Simulator sim in entities)
                {
                    sim.Tick();
                }
                Console.ReadKey();  
                Logger.printInfo("\ntick: " + tick);
            }
            Logger.printInfo("finished");
            Console.ReadKey();
        }

        public void AddSimulator(Simulator sim)
        {
            entities.Add(sim);
        }
    }
}
