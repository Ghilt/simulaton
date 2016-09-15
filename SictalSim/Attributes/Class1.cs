using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    class Needs : Dictionary<int, Need>
    {
        internal void tick()
        {
            foreach (Need need in this.Values)
            {
                need.Tick();
                need.Affect();
            }
        }
    }
}
