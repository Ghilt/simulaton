using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Attributes;

namespace Simulaton.Events
{
    class ExecuteInteractionEvent : Event
    {
        private bool isHandled;
        private InteractionAbility ability;
        private int targetPropertyId;

        public ExecuteInteractionEvent(int propertyId, InteractionAbility ability)
        {
            this.targetPropertyId = propertyId;
            this.ability = ability;
            isHandled = false;
        }

        public Entity GetSender()
        {
            return ability.parent;
        }

        public void Handle(Life context)
        {
            ability.executeInteraction(targetPropertyId, context);
            isHandled = true;
        }

        public bool IsHandled()
        {
            return isHandled;
        }
    }
}
