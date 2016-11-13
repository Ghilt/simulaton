using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    interface Resource
    {
        float Extract(int propertyId);
    }
}
