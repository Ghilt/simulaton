using Simulaton.Simulation;
using System;

namespace Simulaton.Attributes
{
    class Location
    {
        private readonly Region space;
        private int x;
        private int y;

        public Location(Region region, int x, int y)
        {
            this.space = region;
            this.x = x;
            this.y = y;
        }

        internal void Move()
        {
            Random r = new Random();

            x += r.Next(x == 0 ? 0 : -1, x == space.GetWidth() ? 0 : 2);
            y += r.Next(y == 0 ? 0 : -1, y == space.GetLength() ? 0 : 2);
        }

        internal int GetX()
        {
            return x;
        }

        internal int GetY()
        {
            return y;
        }
    }
}