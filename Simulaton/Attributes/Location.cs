﻿using Simulaton.Simulation;
using System;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    public class Location : Resource
    {
        private readonly Region space;
        public int x { get; private set; }
        public int y { get; private set; }

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

        internal void PostEvent(Event e)
        {
            space.PostEvent(e);
        }

        public float Extract(int propertyId)
        {
            return space.Extract(x, y, propertyId);
        }
    }
}