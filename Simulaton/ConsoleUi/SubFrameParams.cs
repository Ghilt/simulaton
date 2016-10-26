using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.ConsoleUi
{
    class SubFrameParams
    {
        public int xAnchor { private set; get; }
        public int yAnchor { private set; get; }
        public ConsoleFrame frame { private set; get; }

        public SubFrameParams(int x, int y, ConsoleFrame frame)
        {
            this.xAnchor = x;
            this.yAnchor = y;
            this.frame = frame;
        }

        internal char GetCharAt(int x, int y)
        {
            return frame.frame[x - xAnchor, y - yAnchor];
        }
    }
}
