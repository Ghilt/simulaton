using Simulaton.Simulation;

namespace Simulaton.Events
{
    public abstract class Event
    {
        public abstract Entity GetSender();

        //All handlers (visitor pattern), standard behavior do nothing
        public virtual void Handle(Life context) { }

        public virtual void Handle(Item item) { }

        public abstract bool IsHandled();
    }
}
