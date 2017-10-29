using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLProgram
{
    public class RemoveSelectedCommand : Command
    {
        internal RemoveSelectedCommand() { }

        public override void Execute()
        {
            TargetDrawing?.DeleteAllSelected();
        }
    }
}
