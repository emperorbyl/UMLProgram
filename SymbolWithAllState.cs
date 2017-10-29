using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace UMLProgram
{
    public class SymbolWithAllState : Symbol
    {
        internal SymbolWithIntrinsicState IntrinsicState { get; }
        public SymbolExtrinsicState ExtrinsicStatic { get; }

        internal SymbolWithAllState(SymbolWithIntrinsicState sharedPart, SymbolExtrinsicState nonsharedPart)
        {
            IntrinsicState = sharedPart;                // From a decorator perspective, this is the decorated object
            ExtrinsicStatic = nonsharedPart;            // From a decorator perspective, this is the added feature or
                                                        // capability that this object (a decorator) is adding
        }

        public override bool IsSelected
        {
            get { return ExtrinsicStatic.IsSelected; }
            set { ExtrinsicStatic.IsSelected = value; }
        }

        public override Point Location
        {
            get { return ExtrinsicStatic.Location; }
            set { ExtrinsicStatic.Location = value; }
        }


        public override Size Size
        {
            get { return ExtrinsicStatic.Size; }
            set { ExtrinsicStatic.Size = value; }
        }

        public override void Draw(Graphics graphics)
        {
            if (graphics == null || IntrinsicState?.Image == null) return;

            graphics.DrawImage(IntrinsicState.Image,
                new Rectangle(ExtrinsicStatic.Location.X, ExtrinsicStatic.Location.Y, ExtrinsicStatic.Size.Width, ExtrinsicStatic.Size.Height),
                0, 0, IntrinsicState.Image.Width, IntrinsicState.Image.Height,
                GraphicsUnit.Pixel);

            if (ExtrinsicStatic.IsSelected)
            {
                graphics.DrawRectangle(
                    SelectedPen,
                    ExtrinsicStatic.Location.X,
                    ExtrinsicStatic.Location.Y,
                    ExtrinsicStatic.Size.Width,
                    ExtrinsicStatic.Size.Height);

                DrawActionHandle(graphics, ExtrinsicStatic.Location.X, ExtrinsicStatic.Location.Y);
                DrawActionHandle(graphics, ExtrinsicStatic.Location.X + ExtrinsicStatic.Size.Width, ExtrinsicStatic.Location.Y);
                DrawActionHandle(graphics, ExtrinsicStatic.Location.X, ExtrinsicStatic.Location.Y + ExtrinsicStatic.Size.Height);
                DrawActionHandle(graphics, ExtrinsicStatic.Location.X + ExtrinsicStatic.Size.Width, ExtrinsicStatic.Location.Y + ExtrinsicStatic.Size.Height);
            }
        }

        private void DrawActionHandle(Graphics graphics, int x, int y)
        {
            graphics.FillRectangle(HandlesBrush, x - HandleHalfSize, y - HandleHalfSize, HandleHalfSize * 2, HandleHalfSize * 2);
        }
    }
}
