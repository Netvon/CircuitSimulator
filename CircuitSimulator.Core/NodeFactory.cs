using CircuitSimulator.Core.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CircuitSimulator.Core
{
    class NodeFactory
    {
		HashSet<Type> nodeTypes = new HashSet<Type>();

		public NodeFactory AddNodeTypes(IEnumerable<Type> types)
		{
			foreach (var type in types.Where(t => t.GetTypeInfo().IsSubclassOf(typeof(Node))))
			{
				nodeTypes.Add(type);
			}

			return this;
		}
	}
}
