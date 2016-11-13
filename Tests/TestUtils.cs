using Simulaton.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class TestUtils
    {
        public const int TEST_POROPERTY_1 = 0;
        public const int TEST_POROPERTY_2 = 1;
        public const int TEST_POROPERTY_3 = 2;

        public static Life CreateTestLifeTarget()
        {
            Life target = new Life(0, "TesterName", null);
            target.AddProperty(new Property(TEST_POROPERTY_1, 0.5f));
            target.AddProperty(new Property(TEST_POROPERTY_2, 0.5f));
            target.AddProperty(new Property(TEST_POROPERTY_3, 0.5f));

            return target;
        }
    }
}
