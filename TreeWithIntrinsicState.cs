﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace UMLProgram
{
    internal class TreeWithIntrinsicState : Tree
    {
        public static Color SelectionBackgroundColor { get; set; } = Color.DarkKhaki;
        public string TreeType { get; set; }
        public Bitmap Image { get; private set; }
        public Bitmap ToolImage { get; private set; }
        public Bitmap ToolImageSelected { get; private set; }

        public void LoadFromResource(string treeType, Type referenceTypeForAssembly)
        {
            if (string.IsNullOrWhiteSpace(treeType)) return;

            Assembly assembly = Assembly.GetAssembly(referenceTypeForAssembly);

            if (assembly == null) return;

            using (Stream stream = assembly.GetManifestResourceStream(treeType))
            {
                if (stream != null)
                {
                    Image = new Bitmap(stream);
                    ToolImage = new Bitmap(Image, ToolSize);
                    ToolImageSelected = new Bitmap(ToolSize.Width, ToolSize.Height);

                    Graphics g = Graphics.FromImage(ToolImageSelected);
                    g.Clear(SelectionBackgroundColor);
                    g.DrawImage(ToolImage, new Point() { X = 0, Y = 0 });
                }
            }
        }

        public override bool IsSelected
        {
            get { return false; }
            set
            {
                throw new ApplicationException("Cannot select a tree with only intrinsic state - the intrinsic state is immutable");
            }
        }


        public override Point Location
        {
            get { return new Point(); }
            set
            {
                throw new ApplicationException("Cannot change a tree with only intrinsic state - the intrinsic state is immutable");
            }
        }

        public override Size Size
        {
            get { return new Size(); }
            set
            {
                throw new ApplicationException("Cannot change a tree with only intrinsic state - the intrinsic state is immutable");
            }
        }

        public override void Draw(Graphics graphics)
        {
            throw new ApplicationException("Cannot draw a tree with only intrinsic state");
        }
    }
}
