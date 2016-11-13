using Simulaton.Events;
using System.Collections.Generic;

namespace Simulaton.Simulation
{
    public abstract class AbilityEvent
    {
        private List<AbilityModifier> modifiers;

        public AbilityEvent()
        {
            this.modifiers = new List<AbilityModifier>();
        }

        public void AddModifier(AbilityModifier modifier)
        {
            modifiers.Add(modifier);
        }

        public float CalculateEffieciencyModifier(Life target)
        {
            float effieciencyModifier = 1;
            modifiers.ForEach(modifier => effieciencyModifier *= modifier.GetModification(target));
            return effieciencyModifier;
        }

        public abstract void Trigger(int propertyIdTrigger, Life target);
        public abstract float GetMagnitude();
        public abstract EvaluableResult EvaluateResult(int targetpropertyId);
    }
}
