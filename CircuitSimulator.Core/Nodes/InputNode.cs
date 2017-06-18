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
			InputCount = 1;
		}

		public InputNode(string name) : base(name)
		{
			InputCount = 1;
		}

		protected override NodeCurrent ProcessOutput(NodeCurrent value)
        {
            return value;
        }
    }
}
