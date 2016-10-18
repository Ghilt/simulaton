using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Attributes;
using Simulaton.Simulation;

namespace Simulaton.Events
{
    class RequestEvent : Event
    {
        private InteractionAbility interactionAbility;
        private int propertyTargetId;
        private bool handled = false;

        public RequestEvent(InteractionAbility ability)
        {
            this.interactionAbility = ability;
        }

        public RequestEvent(int targetId, InteractionAbility interactionAbility)
        {
            this.propertyTargetId = targetId;
            this.interactionAbility = interactionAbility;
        }

        public Entity GetSender()
        {
            return interactionAbility.parent;
        }

        public void Handle(Life context)
        {
            context.brain.MakeDecision(propertyTargetId, interactionAbility);
            handled = true;
        }

        public bool isHandled()
        {
            return handled;
        }
    }
}
