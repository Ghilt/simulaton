﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public class ModifyNeedEffect : Effect
    {
        private float magnitude;
        private float threshold;
        private Need source;
        private Need target;
        private float importance;

        public ModifyNeedEffect(Need source, Need target, float thresholdPercentage, float magnitude, float importance)
        {
            this.source = source;
            this.target = target;
            this.threshold = thresholdPercentage;
            this.magnitude = magnitude;
            this.importance = importance;
        }

        internal int getNeedId()
        {
            return target.id;
        }

        public void OnTrigger()
        {
            if (source.amount < threshold)
            {
                target.Modify(magnitude);
            }
            else if (source.amount > 1 - threshold)
            {
                target.Modify(-magnitude);
            }
        }

        public float GetImportance()
        {
            return importance;
        }
    }
}
