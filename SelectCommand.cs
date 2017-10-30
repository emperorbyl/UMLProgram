using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UMLProgram
{
    public class SelectCommand : Command
    {
        private Element _symbol;
        private bool _originalState;
        private readonly Point _location;
        internal SelectCommand(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
                _location = (Point)commandParameters[0];
        }
        public override bool Execute()
        {
            _symbol = TargetDrawing.FindElementAtPosition(_location);
            if (_symbol == null) return false;

            _originalState = _symbol.IsSelected;
            _symbol.IsSelected = !_originalState;

            TargetDrawing.IsDirty = true;

            return true;
        }

        internal override void Undo()
        {
            if (_symbol.IsSelected == _originalState) return;

            _symbol.IsSelected = _originalState;
            TargetDrawing.IsDirty = true;
        }

        internal override void Redo()
        {
            if (_symbol.IsSelected != _originalState) return;

            _symbol.IsSelected = !_originalState;
            TargetDrawing.IsDirty = true;
        }
    }
}
