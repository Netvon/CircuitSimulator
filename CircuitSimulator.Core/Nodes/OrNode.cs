using System;

namespace CircuitSimulator.Core.Nodes
{
    public class OrNode :  Node
	{
		public override void Step()
		{
			Value = Convert.ToInt32(!Convert.ToBoolean(Value.Value));

			base.Step();
		}
	}
}
