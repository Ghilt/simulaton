using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    class SatisfyFromResourceEvent : SatisfyEvent
    {
        private const int SATISFY_ANY = -1;

        private Resource resource;

        public SatisfyFromResourceEvent(int needSatisfied, Interval magnitude, Resource resource) : base(needSatisfied,magnitude)
        {
            this.resource = resource;
        }

        public SatisfyFromResourceEvent(Interval magnitude, Resource resource) : this(SATISFY_ANY, magnitude, resource) { }

        public override void Trigger(Life owner, int needIdTrigger)
        {
            int realTarget = needSatisfied == SATISFY_ANY ? needIdTrigger : needSatisfied;

            float amount = extractFromSource(realTarget);
            owner.ModifyProperty(realTarget, amount);
            Logger.PrintInfo(this, Logger.Need[realTarget] + ", +" + ((int)(amount * 100) + "%"));
        }

        private float extractFromSource(int needId)
        {
            float amountSatisfiedModifier = (resource == null) ? 1f * magnitude.NextFloat() : resource.Extract(needId) * magnitude.NextFloat();
            return amountSatisfiedModifier;
        }

    }
}
