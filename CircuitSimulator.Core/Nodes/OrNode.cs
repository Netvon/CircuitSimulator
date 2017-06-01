using System;

namespace CircuitSimulator.Core.Nodes
{
    public class NotNode :  Node
	{
		public override void Step()
		{
			Value = Convert.ToInt32(!Convert.ToBoolean(Value.Value));

			base.Step();
		}
	}
}
