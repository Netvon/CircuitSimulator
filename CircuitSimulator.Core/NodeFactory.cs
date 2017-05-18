using CircuitSimulator.Core.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CircuitSimulator.Core
{
	public class NodeFactory
	{
		Dictionary<string, Type> nodeTypes = new Dictionary<string, Type>();

		public NodeFactory AddNodeTypes(IEnumerable<(string name, Type nodeType)> types)
		{
			foreach (var type in types.Where(t => t.nodeType.GetTypeInfo().IsSubclassOf(typeof(Node))))
			{
				nodeTypes.Add(type.name ?? type.nodeType.Name, type.nodeType);
			}

			return this;
		}

		public NodeFactory AddNodeType<TNode>(string name = null) where TNode : Node
		{
			var type = typeof(TNode);
			nodeTypes.Add(name ?? type.Name, typeof(TNode));
			return this;
		}

		public Node Create(string nodeType)
		{
			if(nodeTypes.TryGetValue(nodeType, out var type))
			{
				return Activator.CreateInstance(type) as Node;
			}

			return null;
		}
	}
}
