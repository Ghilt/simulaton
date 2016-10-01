using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class Brain
    {

        public Brain()
        {

        }

        public void MakeDecision(Needs needs, Abilities abilities)
        {

            Need pressingDesire = needs.getMostImportantNeed();
            Logger.PrintInfo(this, "Most pressing need- " + Logger.Need[pressingDesire.id]);
            float mostValue = 0.0f;
            Ability toDo = null;
            foreach (Ability action in abilities.Values)
            {
                float value = action.EvaluateEffectiveness(needs);
                if (value > mostValue)
                {
                    mostValue = value;
                    toDo = action;
                }

            }
            if (toDo != null)
            {
                Logger.PrintInfo(this, "Most fitting action- " + Logger.Ability[toDo.id]);
                toDo.Execute(needs.SortedOnImportance());
            }
        }
    }
}
