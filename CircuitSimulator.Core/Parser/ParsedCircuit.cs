using System;
using System.Collections.Generic;
using System.Linq;

namespace CircuitSimulator.Core.Parser
{
    public class ParsedCircuit
    {
		public List<ParsedNode> Nodes { get; set; } = new List<ParsedNode>();

		public void AddConnection(string nodeA, string nodeB)
		{
			Nodes.Find(node => node.Name == nodeA).Connections.Add(nodeB);
		}
	}
}
