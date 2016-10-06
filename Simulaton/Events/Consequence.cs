using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;

namespace Simulaton.Attributes
{
    interface Consequence
    {
        bool DoesSatisfyNeed(int needId);
        void Trigger(Life target, int needIdTrigger);
        float getMagnitude();
        float EvaluateEffectiveness(Needs needsOfSituation);
    }
}
