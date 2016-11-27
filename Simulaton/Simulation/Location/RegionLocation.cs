using Simulaton.Events;
using System;

namespace Simulaton.Simulation
{
    class RegionLocation : Location, Resource
    {
        private readonly Region region;

        public int x { get; private set; }
        public int y { get; private set; }

        public RegionLocation(Region region, int x, int y)
        {
            this.region = region;
            this.x = x;
            this.y = y;
        }

        public bool TryMove(float direction)
        {
            Random r = new Random();

            x += r.Next(x == 0 ? 0 : -1, x == region.GetWidth() ? 0 : 2);
            y += r.Next(y == 0 ? 0 : -1, y == region.GetLength() ? 0 : 2);
            return true;
        }

        public void PostEvent(Event e)
        {
            region.PostEventToInhabitants(e);
        }

        public float Extract(int propertyId)
        {
            return region.Extract(x, y, propertyId);
        }

        public void OnExit(ProteanEntity entity)
        {
            region.RemoveEntity(entity);
        }

        public void OnEnter(ProteanEntity entity)
        {
            region.AddEntity(entity);
        }
    }
}
