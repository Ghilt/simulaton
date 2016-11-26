using Simulaton;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Events
{
    public abstract class Event
    {
        public abstract Entity GetSender();

        //All handlers (visitor pattern), standard behaviod do nothing
        public virtual void Handle(Life context) { }

        public virtual void Handle(Item item) { }

        public abstract bool IsHandled();
    }
}
