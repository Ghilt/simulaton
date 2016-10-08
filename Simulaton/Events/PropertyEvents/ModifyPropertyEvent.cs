﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public class ModifyPropertyEvent : PropertyEvent
    {
        private float magnitude;
        private float threshold;
        private Property source;
        private Property target;
        private float importance;
        private Func<float, float, bool> condition;

        public ModifyPropertyEvent(Property source, Property target, float magnitude, float importance, float threshold, Func<float, float, bool> condition)
        {
            this.source = source;
            this.target = target;
            this.threshold = threshold;
            this.magnitude = magnitude;
            this.importance = importance;
            this.condition = condition;
        }

        internal int getNeedId()
        {
            return target.id;
        }

        public void Trigger()
        {
            if (condition(source.amount, threshold))
            {
                target.Modify(magnitude);
            }
        }

        public float GetImportance()
        {
            return importance;
        }
    }
}