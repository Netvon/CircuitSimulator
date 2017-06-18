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
		HashSet<Type> nodeTypes = new HashSet<Type>();

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
				typeof(AndNode),
				typeof(InputNode),
				typeof(NandNode),
				typeof(NorNode),
				typeof(NotNode),
				typeof(OrNode),
				typeof(OutputNode),
				typeof(XorNode)
			});
		}

		public CircuitBuilder AddNodeTypes(IEnumerable<Type> types)
		{
			foreach (var type in types.Where(t => t.GetTypeInfo().IsSubclassOf(typeof(Node))))
			{
				nodeTypes.Add(type);
			}

			return this;
		}

		public Task<ParsedCircuit> Build()
		{
			switch (storageType)
			{
				case StorageType.Memory:
					return BuildFromMemory();
				case StorageType.File:
					return BuildFromFile();
				default:
					throw new Exception("No Source was Added");
			}

			Task<ParsedCircuit> BuildFromMemory()
			{
				return Task.FromResult(new CircuitParser().Parse(storageSource));
			}

			Task<ParsedCircuit> BuildFromFile()
			{
				return new CircuitParser()
							.LoadFile(storageSource);
			}
		}
	}
}
