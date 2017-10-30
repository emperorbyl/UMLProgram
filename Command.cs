using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLProgram
{
    public abstract class Command
    {
        public Drawing TargetDrawing { get; set; }      // "Receiver" in the Command Pattern

        public abstract bool Execute();
        internal abstract void Undo();                
        internal abstract void Redo();
    }
}
