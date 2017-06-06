using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CircuitSimulator.Core.Nodes
{
	public class InputNode : Node
	{

        public override void Step(NodeCurrent value)
        {

            this.value = value;

            base.Step(value);

        }

    }
}
