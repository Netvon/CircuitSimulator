using System;

namespace CircuitSimulator.Core.Nodes
{
    public class XorNode :  Node
	{

        //      (More inputs possible)
        //      (Goes by the rule: 'If all inputs are high or if all inputs are low, output a high signal')

        //      Input A  Input B  Output
        //      0        0        1
        //      0        1        0
        //      1        0        0
        //      1        1        1

        public override void Step(NodeCurrent value)
        {

            processOutput(value);

            base.Step(value);

        }


        protected virtual NodeCurrent processOutput(NodeCurrent value)
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

            this.value = output;
            return output;
        }
    }
}
