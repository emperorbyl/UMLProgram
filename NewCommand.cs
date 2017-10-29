using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLProgram
{
    public class NewCommand : Command
    {
        internal NewCommand() { }

        public override void Execute()
        {
            TargetDrawing?.Clear();
        }
    }
}
