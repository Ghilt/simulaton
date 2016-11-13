using Simulaton.Events;
using System.Collections.Generic;

namespace Simulaton.Simulation
{
    public abstract class AbilityEvent
    {
        public List<AbilityModifier> modifiers { get; private set; }

        public AbilityEvent()
        {
            this.modifiers = new List<AbilityModifier>();
        }

        internal void AddModifier(AbilityModifier modifier)
        {
            modifiers.Add(modifier);
        }

        public float CalculateEffieciencyModifier()
        {
            float effieciencyModifier = 1;
            modifiers.ForEach(modifier => effieciencyModifier *= modifier.GetModification());
            return effieciencyModifier;
        }

        public abstract void Trigger(int propertyIdTrigger, Life target);
        public abstract float GetMagnitude();
        public abstract EvaluableResult EvaluateResult(int targetpropertyId);
    }
}
