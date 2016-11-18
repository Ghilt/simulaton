using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simulaton.Mechanics
{

    public interface TransformFunction<Tin, Tout>
    {
        Tout Transform(Tin input);
    }

}
