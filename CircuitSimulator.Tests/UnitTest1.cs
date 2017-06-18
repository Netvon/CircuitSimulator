using System;
using System.Linq;
using CircuitSimulator.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CircuitSimulator.Core.Nodes;
using CircuitSimulator.Core.Parser;

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
OUT	:	PROBE		;

B	:	A			;
A	:	OUT			;");

			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestCircuitParserException()
		{
			var parser = new CircuitParser();
			

			Assert.ThrowsException<CircuitParserException>(() =>
			{
				var hallo = parser.Parse(@"#A0:INPUT_HIGH;
# hallo
A	::	INPUT_HIGH	;
B	:	INPUT_LOW	;
OUT	:	PROBE		;");
			});
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
