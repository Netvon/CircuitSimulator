using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CircuitSimulator.Core.Nodes
{
    public class InputNode : Node
    {
        protected override NodeCurrent processOutput(NodeCurrent value)
        {
            return value;
        }
    }
}
