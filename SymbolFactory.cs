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

        private readonly Dictionary<string, SymbolWithIntrinsicState> _sharedSymbols = new Dictionary<string, SymbolWithIntrinsicState>();

        public SymbolWithAllState GetSymbol(SymbolExtrinsicState extrinsicState)
        {
            string resourceName = string.Format(ResourceNamePattern, extrinsicState.SymbolType);

            SymbolWithIntrinsicState symbolWithIntrinsicState;
            if (_sharedSymbols.ContainsKey(extrinsicState.SymbolType))
                symbolWithIntrinsicState = _sharedSymbols[extrinsicState.SymbolType];
            else
            {
                symbolWithIntrinsicState = new SymbolWithIntrinsicState();
                symbolWithIntrinsicState.LoadFromResource(resourceName, ReferenceType);
                _sharedSymbols.Add(extrinsicState.SymbolType, symbolWithIntrinsicState);
            }

            return new SymbolWithAllState(symbolWithIntrinsicState, extrinsicState);
        }
    }
}
