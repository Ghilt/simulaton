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
            List<Simulator> entitiesToRemove = new List<Simulator>();
            Logger.PrintInfo("Started, press any key to continue");
            Console.ReadKey();
            while (entities.Count != 0)
            {
                tick++;
                foreach (Simulator sim in entities)
                {
                    sim.Tick();
                    int status = sim.CheckStatus();
                    if (status == Simulator.SIMULATION_STATUS_TERMINATED)
                    {
                        entitiesToRemove.Add(sim);
                    }
                }

                foreach (Simulator sim in entitiesToRemove)
                {
                    entities.Remove(sim);
                }
                entitiesToRemove.Clear();

                Console.ReadKey();  
                Logger.PrintInfo("\ntick: " + tick);
            }
            Logger.PrintInfo("finished, press enter to exit");
            Console.ReadLine();
            
        }

        public void AddSimulator(Simulator sim)
        {
            entities.Add(sim);
        }
    }
}
