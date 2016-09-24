﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    interface NeedFactory
    {
        Needs CreateNeeds(Simulator owner);
    }
}
