using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UMLProgram
{
    interface IGraphicsWithSelection
    {
        void DrawSelectionBox(Point location, Size size);
        void DrawSelectionLine(Point start, Point end);
    }
}
