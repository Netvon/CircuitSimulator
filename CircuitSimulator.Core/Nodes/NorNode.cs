using System;

namespace CircuitSimulator.Core.Nodes
{
    public class NorNode :  Node
	{
		public override void Step()
		{
			Value = Convert.ToInt32(!Convert.ToBoolean(Value.Value));

			base.Step();
		}
	}
}
