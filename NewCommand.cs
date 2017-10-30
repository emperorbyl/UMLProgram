using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLProgram
{
    public class NewCommand : Command
    {
        private List<Element> _previousSymbols;
        internal NewCommand() { }

        public override bool Execute()
        {
            _previousSymbols = TargetDrawing.GetCloneOfElements();
            TargetDrawing?.Clear();
            return _previousSymbols != null && _previousSymbols.Count > 0;
        }

        internal override void Undo()
        {
            if (_previousSymbols == null || _previousSymbols.Count == 0) return;

            foreach (var tree in _previousSymbols)
                TargetDrawing?.Add(tree);
        }

        internal override void Redo()
        {
            Execute();
        }
    }
}
