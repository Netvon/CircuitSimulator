using CircuitSimulator.Core.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitSimulator.Core
{
	public class Circuit
	{
		List<Node> nodes;

		public Circuit()
		{
			nodes = new List<Node>();
		}

		public Circuit Add(Node node)
		{
			nodes?.Add(node);
			return this;
		}

		public void Start()
		{
			var first = nodes.FirstOrDefault();

			if (first != null)
			{
				first.Value = 0;
				first.Step();
			}
		}

		public Node this[string name] => nodes.FirstOrDefault(x => x.Name.Equals(name));
	}
}
