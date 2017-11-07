using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;

namespace UMLProgram
{
    public class SymbolWithAllState : Symbol
    {
        public Pen OutlinePen { get; set; } = new Pen(Color.DarkGray);
        public SymbolWithIntrinsicState IntrinsicState { get; }
        [DataMember]
        public SymbolExtrinsicState ExtrinsicState { get; set; }

        public SymbolWithAllState(SymbolWithIntrinsicState sharedPart, SymbolExtrinsicState nonsharedPart)
        {
            IntrinsicState = sharedPart;                // From a decorator perspective, this is the decorated object
            ExtrinsicState = nonsharedPart;            // From a decorator perspective, this is the added feature or
                                                        // capability that this object (a decorator) is adding
        }

        public override bool IsSelected
        {
            get { return ExtrinsicState.IsSelected; }
            set { ExtrinsicState.IsSelected = value; }
        }

        public override Point Location
        {
            get { return ExtrinsicState.Location; }
            set { ExtrinsicState.Location = value; }
        }


        public override Size Size
        {
            get { return ExtrinsicState.Size; }
            set { ExtrinsicState.Size = value; }
        }
        public override Element Clone()
        {
            return new SymbolWithAllState(IntrinsicState, ExtrinsicState = ExtrinsicState.Clone());
        }
        public override void Draw(Graphics graphics)
        {
            if (graphics == null || IntrinsicState?.Image == null) return;

            graphics.DrawImage(IntrinsicState.Image,
                new Rectangle(ExtrinsicState.Location.X, ExtrinsicState.Location.Y, ExtrinsicState.Size.Width, ExtrinsicState.Size.Height),
                0, 0, IntrinsicState.Image.Width, IntrinsicState.Image.Height,
                GraphicsUnit.Pixel);

            if (ExtrinsicState.IsSelected)
            {
                var g = new GraphicsWithSelection() { MyGraphics = graphics };
                g.DrawSelectionBox(ExtrinsicState.Location, ExtrinsicState.Size);
            }
        }
        public override bool ContainsPoint(Point point)
        {
            return point.X >= Location.X && point.Y >= Location.Y &&
                   point.X <= Location.X + Size.Width &&
                   point.Y <= Location.Y + Size.Height;
        }

    }
}
