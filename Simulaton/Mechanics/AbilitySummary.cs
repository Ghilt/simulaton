using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Mechanics
{
    class AbilitySummary : Summary
    {

        private int id;

        public AbilitySummary(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return Ability.Name[id];
        }
    }
}
