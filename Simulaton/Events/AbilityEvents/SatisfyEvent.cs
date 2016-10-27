using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    public class SatisfyEvent : AbilityEvent
    {
        private const int SATISFY_ANY = -1;

        public Interval magnitude { get; private set; }
        public int propertySatisfied { get; private set; }

        public SatisfyEvent(int propertySatisfied, Interval magnitude)
        {
            this.magnitude = magnitude;
            this.propertySatisfied = propertySatisfied;
        }

        public SatisfyEvent(Interval magnitude)
        {
            this.magnitude = magnitude;
            this.propertySatisfied = SATISFY_ANY;
        }

        public virtual void Trigger(int propertyIdTrigger, Life target)
        {
            int realTarget = propertySatisfied == SATISFY_ANY ? propertyIdTrigger : propertySatisfied;

            float amount = extractFromSource(realTarget);
            target.ModifyProperty(realTarget, amount);
            Logger.PrintInfo(this, "\t " + Property.Name[realTarget] + ", " + Logger.FloatToPercentWithSign(amount));
        }

        private float extractFromSource(int propertyId)
        {
            float amountSatisfiedModifier = magnitude.NextFloat();
            return amountSatisfiedModifier;
        }

        public float GetMagnitude()
        {
            return magnitude.getPowerLevel();
        }

        public EvaluableResult EvaluateResult(int targetpropertyId)
        {
            int realTarget = propertySatisfied == SATISFY_ANY ? targetpropertyId : propertySatisfied;
            return new EvaluableResult(realTarget, GetMagnitude());
        }
    }
}
