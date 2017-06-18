using CircuitSimulator.Core.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CircuitSimulator.Core.Validators;

namespace CircuitSimulator.Core
{
	public class Circuit : IVisitable
	{
		public List<Node> nodes;
        public List<InputNode> inputNodes;
        public List<OutputNode> outputNodes;


        public Circuit()
		{
			nodes = new List<Node>();
            inputNodes = new List<InputNode>();
            outputNodes = new List<OutputNode>();
        }

		public Node Find(string name)
		{
			return (nodes.FirstOrDefault(x => x.Name == name) ??
				   inputNodes.FirstOrDefault(x => x.Name == name)) ??
				   outputNodes.FirstOrDefault(x => x.Name == name);
		}

		public Circuit Add(Node node)
		{
			nodes?.Add(node);

            return this;
		}

        public Circuit AddInput(InputNode node, NodeCurrent value)
        {
            node.Value = value;
            inputNodes?.Add(node);

            return this;
        }

        public Circuit AddOutput(OutputNode node)
        {
            outputNodes?.Add(node);

            return this;
        }

        public void Start()
		{
            foreach (var input in inputNodes)
            {
                input.Step(input.Value);
            }

		}

		public void Reset()
		{
			foreach (var node in inputNodes)
			{
				node.InputValues.Clear();
			}

			foreach (var node in nodes.Union(outputNodes))
			{
				node.Reset();
			}
		}

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public Node this[string name] => nodes.FirstOrDefault(x => x.Name.Equals(name));
	}
}
