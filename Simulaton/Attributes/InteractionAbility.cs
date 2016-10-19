using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    class InteractionAbility : Ability
    {
        public InteractionAbility(int id, Life parent) : base(id, parent)
        {
        }

        internal override void Execute(int targetId)
        {
            Logger.PrintInfo(this, "Look to " + Logger.Ability[id] + ", result next tick");
            this.parent.GetLocation().PostEvent(new RequestEvent(targetId, this));
                
        }

        internal void executeInteraction(int propertyTargetId, Life interactor)
        {
            Logger.PrintInfo(this, parent.name + " and " + interactor.name  + " " + Logger.Ability[id]);
            foreach (AbilityEvent consequence in consequences)
            {
                consequence.Trigger(propertyTargetId, parent);
            }

            foreach (AbilityEvent consequence in consequences)
            {
                consequence.Trigger(propertyTargetId, interactor);
            }
        }
    }
}
