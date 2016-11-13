using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public interface PropertyEvent
    {

        void Trigger();

        float GetImportance();
    }
}
