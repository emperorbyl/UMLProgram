﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;

namespace UMLProgram
{
    [DataContract]
    public class SymbolExtrinsicState
    {
        [DataMember]
        public string SymbolType { get; set; }

        [DataMember]
        public Point Location { get; set; }

        [DataMember]
        public Size Size { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }
        public SymbolExtrinsicState Clone()
        {
            return new SymbolExtrinsicState()
            {
                SymbolType = SymbolType,
                Location = Location,
                Size = Size,
                IsSelected = IsSelected
            };
        }
    }
}
