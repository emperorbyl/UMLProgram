using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLProgram
{
    public class RemoveSelectedCommand : Command
    {
        private List<Element> _deletedSymbols;
        internal RemoveSelectedCommand() { }

        public override bool Execute()
        {
            _deletedSymbols = TargetDrawing?.DeleteAllSelected();
            return _deletedSymbols != null && _deletedSymbols.Count > 0;
        }

        internal override void Undo()
        {
            if (_deletedSymbols == null || _deletedSymbols.Count == 0) return;

            foreach (var tree in _deletedSymbols)
                TargetDrawing?.Add(tree);
        }

        internal override void Redo()
        {
            if (_deletedSymbols == null || _deletedSymbols.Count == 0) return;

            foreach (var tree in _deletedSymbols)
                TargetDrawing?.DeleteElement(tree);
        }
    }
}
