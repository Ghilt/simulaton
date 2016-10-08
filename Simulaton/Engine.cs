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
        private List<Entity> entities;

        public Engine()
        {
            tick = 0;
            entities = new List<Entity>();
        }

        public void Start()
        {
            List<Entity> entitiesToRemove = new List<Entity>();
            Logger.PrintInfo(this, "Started, press any key to continue");
            Console.ReadKey(true);
            while (entities.Count != 0)
            {

                Logger.PrintInfo(this, "Tick (" + tick + ")");
                tick++;
                foreach (Entity sim in entities)
                {
                    sim.Tick();
                    int status = sim.CheckStatus();
                    if (status == Entity.SIMULATION_STATUS_TERMINATED)
                    {
                        entitiesToRemove.Add(sim);
                    }
                }

                foreach (Entity sim in entitiesToRemove)
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

        public void AddSimulator(Entity sim)
        {
            entities.Add(sim);
        }
    }
}
