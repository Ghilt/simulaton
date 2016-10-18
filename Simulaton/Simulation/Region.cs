using Simulaton.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{

    // interesting resource http://www.redblobgames.com/grids/hexagons/
    public class Region : Entity
    {
        private int width;
        private int length;
        private Interval resourcesAvailable;

        private List<Entity> entities;

        public Region(int ticksBirth, int width, int length) : base(ticksBirth, "Plain plains, Nevada")
        {
            this.width = width;
            this.length = length;
            entities = new List<Entity>();
            resourcesAvailable = new Interval(0.1f, 0.9f);
        }

        internal int GetWidth()
        {
            return width;
        }

        internal int GetLength()
        {
            return length;
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public override void OnTick()
        {
            
        }

        internal float Extract(int x, int y, int propertyId)
        {
            //TODO
            return resourcesAvailable.NextFloat();
        }

        public override void OnTerminate()
        {
            throw new NotImplementedException();
        }

        public override void OnEvent(Event exteriorEvent)
        {
            foreach (Entity thing in entities) //TODO should not propagate everything to everyone
            {
                if (exteriorEvent.GetSender() != thing)
                {
                    thing.PostEvent(exteriorEvent);
                }
            }
        }
    }
}
