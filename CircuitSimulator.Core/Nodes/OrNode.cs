using System;

namespace CircuitSimulator.Core.Nodes
{
    public class OrNode :  Node
	{

        //      (More inputs possible)
        //      (Goes by the rule: 'If 1 input is high, output a high signal')

        //      Input A  Input B  Output
        //      0        0        0
        //      0        1        1
        //      1        0        1
        //      1        1        1


        protected override NodeCurrent ProcessOutput(NodeCurrent value)
        {
            InputValues.Add(value);

            NodeCurrent output = NodeCurrent.None;
            foreach (NodeCurrent input in InputValues)
            {
                if (input == NodeCurrent.High || output == NodeCurrent.High)
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
