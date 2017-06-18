using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator.Core.Nodes
{
    public abstract class Node
    {
		public Node()
		{

		}

		public Node(string name)
		{
			Name = name;
		}

        public string Name { get; set; }

        public List<Node> OutputNodes = new List<Node>();

        public List<NodeCurrent> InputValues = new List<NodeCurrent>();

        public int InputCount = 0;

        public NodeCurrent Value { get; set; } = NodeCurrent.None;

        protected abstract NodeCurrent ProcessOutput(NodeCurrent value);

        public void Step(NodeCurrent value)
        {
            if (value != NodeCurrent.High && value != NodeCurrent.Low)
            {
                // RECIEVED NONE VALUE, STOP ALL THE THINGS!
                return;
            }

            InputValues.Add(value);

            if(InputCount == InputValues.Count)
            {
				ProcessOutput(value);

				foreach (Node output in OutputNodes)
                {
                    output.Step(Value);
                }
            }
		}

        public void AddOutput(Node output)
        {
            OutputNodes.Add(output);
            output.ConnectedInput();
        }

        public void ConnectedInput()
        {
            InputCount++;
        }

	}
}
