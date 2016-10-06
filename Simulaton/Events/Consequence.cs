using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;
using Simulaton.Events;

namespace Simulaton.Attributes
{
    interface Consequence
    {
        void Trigger(Life target, int needIdTrigger);
        float getMagnitude();
        EvaluableResult EvaluateEffectiveness(int targetNeedId);
    }
}
