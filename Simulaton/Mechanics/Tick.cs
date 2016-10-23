using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    public class Tick
    {
        private int ticks;

        public Tick()
        {
            ticks = 0;
        }

        public static Tick operator ++(Tick t)
        {
            t.ticks++;
            return t;
        }

        internal int Current()
        {
            return ticks;
        }

        public override string ToString()
        {
            return "Tick (" + ticks + ")";
        }
    }
}
