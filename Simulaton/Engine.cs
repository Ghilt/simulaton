using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;

namespace Simulaton
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
            Logger.PrintInfo(this, "Started, press any key to continue");
            Console.ReadKey(true);
            while (entities.Count != 0)
            {

                Logger.PrintInfo(this, "Tick (" + tick + ")");
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

                Logger.PrintInfo(this, "> \n");
                Console.ReadKey(true);
            }
            Logger.PrintInfo(this, "finished, press enter to exit");
            Console.ReadLine();

        }

        public void AddSimulator(Simulator sim)
        {
            entities.Add(sim);
        }
    }
}
