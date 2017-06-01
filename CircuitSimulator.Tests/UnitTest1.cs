using System;
using System.Linq;
using CircuitSimulator.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CircuitSimulator.Core.Nodes;

namespace CircuitSimulator.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var parser = new CircuitParser();
			var hallo = parser.ParseLine("A0:	INPUT_HIGH   ;");

			Assert.IsTrue(hallo.HasValue);
		}

		[TestMethod]
		public void TestMethod2()
		{
			var parser = new CircuitParser();
			var hallo = parser.Parse(@"#A0:INPUT_HIGH;
# hallo
A	:	INPUT_HIGH	;
B	:	INPUT_LOW	;
OUT	:	PROBE		;").ToDictionary(v => v.name, v => v.value);

			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestMethod5()
		{
			var parser = new CircuitParser();
			var hallo = parser.ParseToDictionary(@"#A0:INPUT_HIGH;
# hallo
A	::	INPUT_HIGH	;
B	:	INPUT_LOW	;
OUT	:	PROBE		;");

			Assert.IsTrue(true);
		}

		//[TestMethod]
		//public void TestMethod3()
		//{
		//	var and = new And
		//	{
		//		InputA = 1,
		//		InputB = 1
		//	};

		//	Assert.AreEqual(0b1, and.Apply());
		//}

		[TestMethod]
		public void TestMethod4()
		{
			//var v1 = InputNode.INPUT_HIGH;
			//var v2 = InputNode.INPUT_HIGH;

			//var and = new And();

			//v1.Outputs.Add(and);
			//v2.Outputs.Add(and);
		}

		[TestMethod]
		public async Task TestMethod6()
		{
			var builder = new CircuitBuilder()
				.AddInMemorySource(@"# Hallo There
									In1: INPUT_HIGH;
									In2: INPUT_LOW;
									Out1: PROBE;")
				.AddDefaultNodes();

			var c = await builder.Build();
		}

		[TestMethod]
		public void MyTestMethod7()
		{
			var n1 = new NotNode();
			var n2 = new NotNode();
			var n3 = new NotNode();

			n1.Value = 0;
			n1.OutputNode = n2;
			n2.OutputNode = n3;

			n1.Step();
		}

		[TestMethod]
		public void TestFactory()
		{
			var factory = new NodeFactory();

			var node = factory.AddNodeType<AndNode>()
					.Create(nameof(AndNode));

			Assert.IsTrue(node is AndNode);

			var node2 = factory.Create("nope");

			Assert.IsNull(node2);
		}
	}
}
