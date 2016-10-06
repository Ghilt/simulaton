using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Events
{
    class EvaluableResult
    {
        public int needId { private set; get; }
        public float magnitude { private set; get; }

        public EvaluableResult(int needId, float magnitude)
        {
            this.needId = needId;
            this.magnitude = magnitude;
        }
    }
}
