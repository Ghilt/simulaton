using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    class Consequence
    {
        public int needId { get; private set; }
        public float magnitude { get; private set; }
        private Ability parent;


        public Consequence(int needId, Ability parent,  float magnitude)
        {
            this.needId = needId;
            this.parent = parent;
            this.magnitude = magnitude;

        }

    }
}
