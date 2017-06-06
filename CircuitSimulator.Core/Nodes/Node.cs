using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator.Core.Nodes
{
    public abstract class Node
    {
        public string Name { get; set; }

        public List<Node> OutputNodes = new List<Node>();

        public List<NodeCurrent> InputValues = new List<NodeCurrent>();

        public int InputCount = 0;

        public NodeCurrent value { get; set; } = NodeCurrent.None;


        public virtual void Step(NodeCurrent value)
        {
            if (value != NodeCurrent.High && value != NodeCurrent.Low)
            {
                // RECIEVED NONE VALUE, STOP ALL THE THINGS!
                return;
            }

            if(InputCount == OutputNodes.Count)
            {
                foreach (Node output in OutputNodes)
                {
                    output.Step(this.value);
                }
            }
		}

        public void addOutput(Node output)
        {
            OutputNodes.Add(output);
            output.connectedInput();
        }

        public void connectedInput()
        {
            InputCount++;
        }





	}
}
