﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osmrt.app
{
    public interface ILoaded
    {
        EventHandler OnSearchEvent { get; set; }
        void OnProgressStart(String message);
        void OnProgressEnd();
    }
}
