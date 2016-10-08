using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Events
{
    class EvaluableResult
    {
        public int propertyId { private set; get; }
        public float magnitude { private set; get; }

        public EvaluableResult(int propertyId, float magnitude)
        {
            this.propertyId = propertyId;
            this.magnitude = magnitude;
        }
    }
}
