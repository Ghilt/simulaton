using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.DataInterface
{
    class TextSummary : Summary
    {
        string text;

        public TextSummary(String text)
        {
            this.text = text;
        }

        public override string ToString()
        {
            return text;
        }
    }
}
