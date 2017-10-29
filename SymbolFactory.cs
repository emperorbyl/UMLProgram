using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLProgram
{
    public class SymbolFactory
    {
        private static SymbolFactory _instance;
        private static readonly object MyLock = new object();

        private SymbolFactory() { }

        public static SymbolFactory Instance
        {
            get
            {
                lock (MyLock)
                {
                    if (_instance == null)
                        _instance = new SymbolFactory();
                }
                return _instance;
            }
        }

        public string ResourceNamePattern { get; set; }
        public Type ReferenceType { get; set; }

        private readonly Dictionary<string, SymbolWithIntrinsicState> _sharedTrees = new Dictionary<string, SymbolWithIntrinsicState>();

        public SymbolWithAllState GetTree(SymbolExtrinsicState extrinsicState)
        {
            string resourceName = string.Format(ResourceNamePattern, extrinsicState.TreeType);

            SymbolWithIntrinsicState treeWithIntrinsicState;
            if (_sharedTrees.ContainsKey(extrinsicState.TreeType))
                treeWithIntrinsicState = _sharedTrees[extrinsicState.TreeType];
            else
            {
                treeWithIntrinsicState = new SymbolWithIntrinsicState();
                treeWithIntrinsicState.LoadFromResource(resourceName, ReferenceType);
                _sharedTrees.Add(extrinsicState.TreeType, treeWithIntrinsicState);
            }

            return new SymbolWithAllState(treeWithIntrinsicState, extrinsicState);
        }
    }
}
