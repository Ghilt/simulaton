using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    interface Effect
    {

        void OnTrigger(Need source);
    }
}
