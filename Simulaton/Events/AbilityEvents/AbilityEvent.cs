using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    interface AbilityEvent
    {
        void Trigger(Life target, int needIdTrigger);
        float GetMagnitude();
        EvaluableResult EvaluateResult(int targetNeedId);
    }
}
