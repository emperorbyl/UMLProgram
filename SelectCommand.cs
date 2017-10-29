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
        private readonly Point _location;

        internal SelectCommand(params object[] commandParameters)
        {
            if (commandParameters.Length > 0)
                _location = (Point)commandParameters[0];
        }

        public override void Execute()
        {
            TargetDrawing?.SelectTreeAtPosition(_location);
        }
    }
}
