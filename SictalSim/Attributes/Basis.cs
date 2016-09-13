using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SictalSim.Attributes
{
    class Basis
    {
        public static int ID_HEALTH = 1;
        private float amount;
        private int id;

        public Basis(int id, float amount)
        {
            this.id = id;
            this.amount = amount;
        }

        internal int getId()
        {
            return id;
        }

        public void Modify(float magnitude)
        {
            float relevantLimit = magnitude > 0 ? 1 : 0;
            float newValue = amount + magnitude;
            bool isOutOfLimit = relevantLimit == 1 ? newValue < relevantLimit : newValue > relevantLimit;
            amount = isOutOfLimit ? newValue : relevantLimit;
        }

        public override string ToString()
        {
            return amount * 100 + "%";
        }
    }
}
