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

        public override Entity GetSender()
        {
            return interactionAbility.parent;
        }

        public override void Handle(Life context)
        {
            context.brain.MakeDecision(propertyTargetId, interactionAbility);
            handled = true;
        }

        public override bool IsHandled()
        {
            return handled;
        }
    }
}
