using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Simulation
{
    public class AbilityModifier
    {
        private Life target;
        private int propertyId;
        private float modifier;
        private Func<float, bool> condition;

        public AbilityModifier(Life target, int propertyId, Func<float, bool> condition, float modifier)
        {
            this.target = target;
            this.propertyId = propertyId;
            this.modifier = modifier;
            this.condition = condition;
        }

        internal float GetModification()
        {
            float valueOfProperty;
            bool exists = target.TryGetPropertyValue(propertyId, out valueOfProperty);
            return (exists && condition(valueOfProperty) ? modifier : 1f);
        }
    }
}
