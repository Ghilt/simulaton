using Simulaton;
using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Events
{
    public interface Event
    {
        Entity GetSender();

        void Handle(Life context);

        bool IsHandled();
    }
}
