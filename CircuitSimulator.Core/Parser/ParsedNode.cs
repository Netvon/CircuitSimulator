using CircuitSimulator.Core.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator.Core.Parser
{
    public class ParsedNode
    {
		public string Name { get; set; }
		public string Value { get; set; }

		public List<string> Connections { get; set; } = new List<string>();
	}
}
