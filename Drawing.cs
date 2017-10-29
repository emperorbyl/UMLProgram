using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;

namespace UMLProgram
{
    public class Drawing
    {
        private static readonly DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(List<SymbolExtrinsicState>));

        private readonly List<Symbol> _trees = new List<Symbol>();

        private readonly object _myLock = new object();

        public Symbol SelectedTree { get; set; }
        public bool IsDirty { get; set; }

        public void Clear()
        {
            lock (_myLock)
            {
                _trees.Clear();
                IsDirty = true;
            }
        }

        public void Add(Symbol tree)
        {
            if (tree != null)
            {
                lock (_myLock)
                {
                    _trees.Add(tree);
                    IsDirty = true;
                }
            }
        }

        public void RemoveTree(Symbol tree)
        {
            if (tree != null)
            {
                lock (_myLock)
                {
                    if (SelectedTree == tree)
                        SelectedTree = null;
                    _trees.Remove(tree);
                    IsDirty = true;
                }
            }
        }

        public void SelectTreeAtPosition(Point location)
        {
            if (SelectedTree != null)
                SelectedTree.IsSelected = false;

            SelectedTree = FindTreeAtPosition(location);

            if (SelectedTree != null)
                SelectedTree.IsSelected = true;

            IsDirty = true;
        }

        public Symbol FindTreeAtPosition(Point location)
        {
            Symbol result;
            lock (_myLock)
            {
                result = _trees.FindLast(t => location.X >= t.Location.X &&
                                              location.X < t.Location.X + t.Size.Width &&
                                              location.Y >= t.Location.Y &&
                                              location.Y < t.Location.Y + t.Size.Height);
            }
            return result;
        }

        public void DeselectAll()
        {
            lock (_myLock)
            {
                foreach (var t in _trees)
                    t.IsSelected = false;
                IsDirty = true;
                SelectedTree = null;
            }
        }

        public void DeleteAllSelected()
        {
            lock (_myLock)
            {
                _trees.RemoveAll(t => t.IsSelected);
            }
        }

        public void RemoveSelectedTree()
        {
            if (SelectedTree != null)
            {
                RemoveTree(SelectedTree);
                SelectedTree = null;
            }
        }

        public bool Draw(Graphics graphics)
        {
            bool didARedraw = false;
            lock (_myLock)
            {
                if (IsDirty)
                {
                    graphics.Clear(Color.White);
                    foreach (var t in _trees)
                        t.Draw(graphics);
                    IsDirty = false;
                    didARedraw = true;
                }
            }
            return didARedraw;
        }

        public void LoadFromStream(Stream stream)
        {
            var extrinsicStates = JsonSerializer.ReadObject(stream) as List<SymbolExtrinsicState>;
            if (extrinsicStates == null) return;

            lock (_myLock)
            {
                _trees.Clear();
                foreach (SymbolExtrinsicState extrinsicState in extrinsicStates)
                {
                    Symbol tree = SymbolFactory.Instance.GetTree(extrinsicState);
                    _trees.Add(tree);
                }
                IsDirty = true;
            }
        }

        public void SaveToStream(Stream stream)
        {
            var extrinsicStates = new List<SymbolExtrinsicState>();
            lock (_myLock)
            {
                foreach (Symbol tree in _trees)
                {
                    SymbolWithAllState t = tree as SymbolWithAllState;
                    if (t != null)
                        extrinsicStates.Add(t.ExtrinsicStatic);
                }
            }
            JsonSerializer.WriteObject(stream, extrinsicStates);
        }
    }
}
