using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CircuitSimulator.Core.Nodes;

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
				typeof(NotNode)
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

		public Task<Circuit> Build()
		{
			switch (storageType)
			{
				case StorageType.Memory:
					return BuildFromMemory();
				case StorageType.File:
					return BuildFromFile();
				default:
					return Task.FromException<Circuit>(new Exception("No Source was Added"));
			}

			Task<Circuit> BuildFromMemory()
			{
				var parser = new CircuitParser()
							.Parse(storageSource);

				return Task.FromResult(new Circuit());
			}

			Task<Circuit> BuildFromFile()
			{
				return Task.FromResult(new Circuit());
			}
		}
	}
}
