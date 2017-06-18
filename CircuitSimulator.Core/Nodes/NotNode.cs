using System;

namespace CircuitSimulator.Core.Nodes
{
    public class NotNode :  Node
	{
		public NotNode()
		{

		}

		public NotNode(string name) : base(name)
		{
		}

		//      (Only 1 input possible)
		//      (Goes by the rule: 'Output the opposite value')

		//      Input A   Output
		//      0         1
		//      1         0



		protected override NodeCurrent ProcessOutput(NodeCurrent value)
        {
            InputValues.Add(value);

            NodeCurrent output = NodeCurrent.None;
            
            if(value == NodeCurrent.Low)
            {
                output = NodeCurrent.High;
            }
            else
            {
                output = NodeCurrent.Low;
            }

            Value = output;
            return output;
        }
    }
}
