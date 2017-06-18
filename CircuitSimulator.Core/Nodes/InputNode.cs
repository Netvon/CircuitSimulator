using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CircuitSimulator.Core.Nodes
{
    public class InputNode : Node
    {
		public InputNode()
		{

		}

		public InputNode(string name) : base(name)
		{
		}

		protected override NodeCurrent ProcessOutput(NodeCurrent value)
        {
            return value;
        }
    }
}
