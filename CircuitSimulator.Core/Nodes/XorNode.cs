using System;

namespace CircuitSimulator.Core.Nodes
{
    public class XorNode :  Node
	{
		public XorNode()
		{

		}

		public XorNode(string name) : base(name)
		{
		}

		//      (More inputs possible)
		//      (Goes by the rule: 'If all inputs are high or if all inputs are low, output a high signal')

		//      Input A  Input B  Output
		//      0        0        1
		//      0        1        0
		//      1        0        0
		//      1        1        1


		protected override NodeCurrent ProcessOutput(NodeCurrent value)
        {
            InputValues.Add(value);

            NodeCurrent output = NodeCurrent.None;
            NodeCurrent valueForHighCurrent = NodeCurrent.None;
            foreach (NodeCurrent input in InputValues)
            {

                if (valueForHighCurrent == NodeCurrent.None)
                {
                    valueForHighCurrent = input;
                    output = NodeCurrent.High;
                }
                else if(input == valueForHighCurrent && output == NodeCurrent.High)
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
