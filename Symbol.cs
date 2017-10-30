using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;

namespace UMLProgram
{
    [DataContract]
    public abstract class Symbol : Element
    {
        public static Pen SelectedPen { get; set; } = new Pen(Color.DarkGray);
        public static Size ToolSize { get; set; } = new Size() { Width = 64, Height = 64 };

        public virtual Point Location { get; set; } = new Point(0, 0);
        public virtual Size Size { get; set; } = new Size(0, 0);
    }
}
