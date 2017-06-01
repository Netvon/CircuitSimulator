using System;

namespace CircuitSimulator.Core.Nodes
{
    public class NotNode :  Node
	{

        //      (Only 1 input possible)
        //      (Goes by the rule: 'Output the opposite value')

        //      Input A   Output
        //      0         1
        //      1         0
 

        public override void Step(NodeCurrent value)
        {
            if (InputValues.Count != 0)
            {
                // RECIEVED A SECOND INPUT, ABORT! ABORT!
                return;
            }

            processOutput(value);

            base.Step(value);

        }


        protected virtual NodeCurrent processOutput(NodeCurrent value)
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

            this.value = output;
            return output;
        }
    }
}
