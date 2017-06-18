using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CircuitSimulator.Core.Nodes
{
    public class OutputNode : Node
    {
		public OutputNode()
		{

		}

		public OutputNode(string name) : base(name)
		{
		}

		protected override NodeCurrent ProcessOutput(NodeCurrent value)
        {
            return value;
        }
    }
}
