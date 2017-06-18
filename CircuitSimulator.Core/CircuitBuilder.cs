using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CircuitSimulator.Core.Nodes;
using CircuitSimulator.Core.Parser;

namespace CircuitSimulator.Core
{
	public class CircuitBuilder
	{
		enum StorageType
		{
			Memory,
			File
		}

		StorageType storageType;
		string storageSource;
		Dictionary<string, Type> nodeTypes = new Dictionary<string, Type>();

		public CircuitBuilder AddFileSource(string path)
		{
			storageType = StorageType.File;
			storageSource = path;

			return this;
		}

		public CircuitBuilder AddInMemorySource(string source)
		{
			storageSource = source;
			storageType = StorageType.Memory;

			return this;
		}

		public CircuitBuilder AddDefaultNodes()
		{
			return AddNodeTypes(new[]
			{
				("and", typeof(AndNode)),
				("input_high", typeof(InputNode)),
				("input_low", typeof(InputNode)),
				("nand", typeof(NandNode)),
				("nor", typeof(NorNode)),
				("not", typeof(NotNode)),
				("or", typeof(OrNode)),
				("probe", typeof(OutputNode)),
				("xor", typeof(XorNode))
			});
		}

		public CircuitBuilder AddNodeTypes(
			IEnumerable<(string selector, Type type)> types
		)
		{
			foreach (var type in types.Where(t => t.type.GetTypeInfo().IsSubclassOf(typeof(Node))))
			{
				nodeTypes.Add(type.selector, type.type);
			}

			return this;
		}

		public async Task<Circuit> Build()
		{
			var circuit = new Circuit();
			ParsedCircuit parsedCircuit = null;

			switch (storageType)
			{
				case StorageType.Memory:
					parsedCircuit = await BuildFromMemory();
					break;
				case StorageType.File:
					parsedCircuit = await BuildFromFile();
					break;
				default:
					throw new Exception("No Source was Added");
			}

			CreateNodes(parsedCircuit);

			return circuit;

			Task<ParsedCircuit> BuildFromMemory()
			{
				return Task.FromResult(new CircuitParser().Parse(storageSource));
			}

			async Task<ParsedCircuit> BuildFromFile()
			{
				return await new CircuitParser().LoadFile(storageSource);
				
			}

			void CreateNodes(ParsedCircuit parsed)
			{
				var nodes = parsed.Nodes.Select(node =>
				{
					if (nodeTypes.TryGetValue(node.Value.ToLower(), out var type))
						return Activator.CreateInstance(type, new[] { node.Name }) as Node;

					return null;
				}).ToList();

				foreach (var node in nodes)
				{
					switch(node)
					{
						case InputNode n:
							Enum.TryParse<NodeCurrent>(
								parsed.Find(n.Name).Value.ToLower().Replace("input_", ""),
								ignoreCase: true,
								result: out var current);

							circuit.AddInput(n, current);
							break;

						case OutputNode n:
							circuit.AddOutput(n);
							break;

						case Node n:
							circuit.Add(n);
							break;
					}
				}

				foreach (var node in parsed.Nodes)
				{
					var nodeA = circuit.Find(node.Name);

					if (nodeA != null) { 
						node.Connections.ForEach(x =>
						{
							var nodeB = circuit.Find(x);
							if (nodeB != null)
								nodeA.AddOutput(nodeB);
						});
					}
					
				}
			}
		}
	}
}
