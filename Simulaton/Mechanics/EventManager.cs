using Simulaton.Events;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics
{
    class EventManager
    {

        private List<Event> events;
        private List<Event> eventsForNextTick;
        private Entity owner;

        public EventManager(Entity owner)
        {
            this.owner = owner;
            events = new List<Event>();
            eventsForNextTick = new List<Event>();
        }

        public void TriggerEvents()
        {
            for (int i = events.Count - 1; i >= 0; i--)
            {
                if (!events[i].IsHandled())
                {
                    owner.OnEvent(events[i]);
                }
                else
                {
                    events.RemoveAt(i);
                }
            }
        }

        internal void Add(Event e)
        {
            eventsForNextTick.Add(e);
        }

        internal void UpdateEvents()
        {
            events.AddRange(eventsForNextTick);
            eventsForNextTick.Clear();
        }
    }
}
