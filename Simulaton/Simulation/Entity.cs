using Simulaton.Events;
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

        public List<Event> events { private set; get; }
        public string name { private set; get; }

        public Entity(int ticksBirth, string name)
        {
            events = new List<Event>();
            this.name = name;
            this.ticksBirth = ticksBirth;
            simulationStatus = SIMULATION_STATUS_ALIVE;
            loggActive = true;
        }

        public void Tick()
        {
            //if (loggActive) Logger.PrintInfo(this, GetCurrentInfoLog());
            ticksAlive++;
            OnTick();
            for (int i = events.Count - 1; i >= 0; i--)
            {
                if (!events[i].isHandled())
                {
                    OnEvent(events[i]);
                } 
                else
                {
                    events.RemoveAt(i);
                }
            }
        }

        public abstract void OnTick();

        public abstract void OnEvent(Event exteriorEvent);

        internal int CheckStatus()
        {
            return simulationStatus;
        }

        public void PostEvent(Event e){
            events.Add(e);
        }

        public void Terminate()
        {
            simulationStatus = SIMULATION_STATUS_TERMINATED;
            OnTerminate();
        }

        public abstract void OnTerminate();

    }
}
