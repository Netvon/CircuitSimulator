using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CircuitSimulator.Core.Nodes
{
	public class AndNode : Node
	{
		public AndNode()
		{

		}

		public AndNode(string name) : base(name)
		{
		}

		//      (More inputs possible)
		//      (Goes by the rule: 'If all inputs are high, output a high signal')

		//      Input A  Input B  Output
		//      0        0        0
		//      0        1        0
		//      1        0        0
		//      1        1        1


		protected override NodeCurrent ProcessOutput(NodeCurrent value)
        {

            NodeCurrent output = NodeCurrent.None;
            foreach (NodeCurrent input in InputValues)
            {
                if(input == NodeCurrent.High && output != NodeCurrent.Low)
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
