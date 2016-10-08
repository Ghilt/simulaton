using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    public abstract class Entity 
    {
        public static int SIMULATION_STATUS_ALIVE = 0;
        public static int SIMULATION_STATUS_TERMINATED = 1;

        private readonly int ticksBirth;
        private int ticksAlive;
        private bool loggActive;
        private int simulationStatus;

        public Entity(int ticksBirth)
        {
            this.ticksBirth = ticksBirth;
            simulationStatus = SIMULATION_STATUS_ALIVE;
            loggActive = true;
        }

        public void Tick()
        {
            if (loggActive) Logger.PrintInfo(this, GetCurrentInfoLog());
            ticksAlive++;
            OnTick();
        }

        public abstract void OnTick();

        public abstract string GetCurrentInfoLog();

        internal int CheckStatus()
        {
            return simulationStatus;
        }

        public void Terminate()
        {
            simulationStatus = SIMULATION_STATUS_TERMINATED;
            OnTerminate();
        }

        public abstract void OnTerminate();

    }
}
