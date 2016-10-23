using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    public class Summary
    {
        public const int TYPE_PROPERTY = 0;

        private float amount;
        private int type;
        private int id;

        public Summary(int type, int id, float amount)
        {
            this.type = type;
            this.id = id;
            this.amount = amount;
        }

        public override string ToString()
        {
            return Logger.Property[id] + " " + Logger.FloatToPercent(amount);
        }
    }
}
