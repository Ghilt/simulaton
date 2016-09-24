using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class Abilities : Dictionary<int, Ability>
    {
        internal void ActUpon(int needId)
        {
            foreach (Ability ability in this.Values) // TODO: Maybe maintain datastructure in which you can lookup correct ability in O(1) || OR give need objects direct access to abilities which affect them
            {
                if (ability.DoesSatisfyNeed(needId))
                {
                    ability.Execute();
                }

            }
        }
    }
}
