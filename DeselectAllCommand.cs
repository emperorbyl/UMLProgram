using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLProgram
{
    public class DeselectAllCommand : Command
    {
        private List<Element> _selectedSymbols;
        internal DeselectAllCommand() { }

        public override bool Execute()
        {
            _selectedSymbols = TargetDrawing?.DeselectAll();
            return _selectedSymbols != null && _selectedSymbols.Count > 0;
        }
        internal override void Undo()
        {
            if (_selectedSymbols == null || _selectedSymbols.Count == 0) return;

            foreach (var tree in _selectedSymbols)
            {
                if (!tree.IsSelected)
                {
                    tree.IsSelected = true;
                    TargetDrawing.IsDirty = true;
                }
            }

        }
        internal override void Redo()
        {
            if (_selectedSymbols == null || _selectedSymbols.Count == 0) return;

            foreach (var tree in _selectedSymbols)
            {
                if (tree.IsSelected)
                {
                    tree.IsSelected = false;
                    TargetDrawing.IsDirty = true;
                }
            }
        }
    }
}
