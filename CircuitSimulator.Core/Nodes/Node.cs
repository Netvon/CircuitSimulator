using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator.Core.Nodes
{
    public abstract class Node
    {
        public string Name { get; set; }

        protected List<Node> OutputNodes = new List<Node>();

        protected List<NodeCurrent> InputValues = new List<NodeCurrent>();



        public virtual void Step(NodeCurrent value)
        {
            if (value != NodeCurrent.High && value != NodeCurrent.Low)
            {
                // RECIEVED NONE VALUE, STOP ALL THE THINGS!
                return;
            }

            if(InputValues.Count == OutputNodes.Count)
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

        }




	}
}
