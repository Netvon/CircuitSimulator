using System;

namespace CircuitSimulator.Core.Nodes
{
    public class NorNode :  Node
	{
		public NorNode()
		{

		}

		public NorNode(string name) : base(name)
		{
		}

		//      (More inputs possible)
		//      (Goes by the rule: 'If 1 input is high, output a low signal')

		//      Input A  Input B  Output
		//      0        0        1
		//      0        1        0
		//      1        0        0
		//      1        1        0


		protected override NodeCurrent ProcessOutput(NodeCurrent value)
        {
            InputValues.Add(value);

            NodeCurrent output = NodeCurrent.None;
            foreach (NodeCurrent input in InputValues)
            {
                if (input == NodeCurrent.Low && output != NodeCurrent.Low)
                {
                    output = NodeCurrent.High;
                }
                else
                {
                    output = NodeCurrent.Low;
                }

            }

            Value = output;
            return output;
        }
    }
}
