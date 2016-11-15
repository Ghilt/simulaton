using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulaton.Simulation;

namespace Simulaton
{
    class SummaryFactory
    {

        internal static Summary CreateSummary(Property property)
        {
            PropertySummary summary = new PropertySummary(property.id, property.amount, property.IsBounded());
            return summary;
        }

        internal static Summary CreateSummary(Ability property)
        {
            AbilitySummary summary = new AbilitySummary(property.id);
            return summary;
        }

    }
}
