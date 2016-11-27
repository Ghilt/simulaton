using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public interface Holder
    {
        void Attach(ProteanEntity proteanEntity);
        void Detach(ProteanEntity proteanEntity);
    }
}
