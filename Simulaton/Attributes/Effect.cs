﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulaton.Attributes
{
    public interface Effect
    {

        void OnTrigger(Need source);

        float GetImportance();
    }
}
