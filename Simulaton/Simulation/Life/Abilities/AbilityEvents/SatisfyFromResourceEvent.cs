using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Simulation
{
    class SatisfyFromResourceEvent : SatisfyEvent
    {
        private const int SATISFY_ANY = -1;

        private Resource resource;

        public SatisfyFromResourceEvent(int propertySatisfied, Interval magnitude, Resource resource) : base(propertySatisfied, magnitude)
        {
            this.resource = resource;
        }

        public SatisfyFromResourceEvent(Interval magnitude, Resource resource) : this(SATISFY_ANY, magnitude, resource) { }

        public override void Trigger(int propertyIdTrigger, Life target)
        {
            int realTarget = propertySatisfied == SATISFY_ANY ? propertyIdTrigger : propertySatisfied;
            float effieciency = CalculateEffieciencyModifier();

            float amount = extractFromSource(realTarget) * effieciency;
            target.ModifyProperty(realTarget, amount);
            Logger.PrintInfo(this, "\t " + Property.Name[realTarget] + ", " + Logger.FloatToPercentWithSign(amount) + " Efficiency: " + Logger.FloatToPercent(effieciency));
        }

        private float extractFromSource(int propertyId)
        {
            float amountSatisfiedModifier = (resource == null) ? 1f * magnitude.NextFloat() : resource.Extract(propertyId) * magnitude.NextFloat();
            return amountSatisfiedModifier;
        }

    }
}
