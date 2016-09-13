using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim
{
    abstract class Simulator 
    {
        private readonly int ticksBirth;
        private int ticksAlive;
        private bool loggActive;

        public Simulator(int ticksBirth)
        {
            this.ticksBirth = ticksBirth;
            loggActive = true;
        }

        public void Tick()
        {
            ticksAlive++;
            PerformTick();
            if (loggActive) Logger.printInfo(GetCurrentInfoLog()); 
        }

        public abstract void PerformTick();

        public abstract string GetCurrentInfoLog();

        public abstract void Terminate();
    }
}
