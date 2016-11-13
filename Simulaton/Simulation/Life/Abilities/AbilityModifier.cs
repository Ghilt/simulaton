using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class AbilityModifier
    {
        private int propertyId;
        private float modifier;
        private Func<float, bool> condition;

        public AbilityModifier(int propertyId, Func<float, bool> condition, float modifier)
        {
            this.propertyId = propertyId;
            this.modifier = modifier;
            this.condition = condition;
        }

        internal float GetModification(Life target)
        {
            float valueOfProperty;
            bool exists = target.TryGetPropertyValue(propertyId, out valueOfProperty);
            return (exists && condition(valueOfProperty) ? modifier : 1f);
        }
    }
}
