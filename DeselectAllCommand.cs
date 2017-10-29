﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLProgram
{
    public class DeselectAllCommand : Command
    {
        internal DeselectAllCommand() { }

        public override void Execute()
        {
            TargetDrawing?.DeselectAll();
        }
    }
}
