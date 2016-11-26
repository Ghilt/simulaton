using Simulaton.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public interface AbilityModifier
    {

        float GetModification(Life target);
    }
}
