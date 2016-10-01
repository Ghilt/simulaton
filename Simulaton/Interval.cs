using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton
{
    class Interval
    {
        //private static Random rand = new Random();
        private static Random random = new Random();

        private float min;
        private float max;
        private float range;
        private int luckModifier;


        public Interval(float min, float max)
        {
            this.min = min;
            this.max = max;
            this.range = max - min;
        }

        public Interval(float min, float max, int luckModifier) : this(min, max)
        {
            this.luckModifier = luckModifier;
        }

        public float NextFloat()
        {
            bool negativeLuck = luckModifier < 0;
            if (negativeLuck) luckModifier *= -1;
            double rand = random.NextDouble();
            for (int i = 0; i < luckModifier; i++)
            {
                double contender = random.NextDouble();
                if (negativeLuck)
                {
                    rand = contender > rand ? rand : contender;
                }
                else
                {
                    rand = rand > contender ? rand : contender;
                }
            }
            //value = min + (float) rand * range;
            return min + (float)rand * range;
        }

        internal float getPowerLevel()
        {
            return min + range / 2;
        }
    }
}
