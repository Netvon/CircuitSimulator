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
OUT	:	PROBE		;

B	:	A			;
A	:	OUT			;").ToDictionary(v => v.name, v => v.value);

			Assert.IsTrue(true);
		}

		[TestMethod]
		public void TestCircuitParserException()
		{
			var parser = new CircuitParser();
			

			Assert.ThrowsException<CircuitParserException>(() =>
			{
				var hallo = parser.ParseToDictionary(@"#A0:INPUT_HIGH;
# hallo
A	::	INPUT_HIGH	;
B	:	INPUT_LOW	;
OUT	:	PROBE		;");
			});
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
		public void AndNodeOutputTest()
		{
			var a1 = new AndNode();
            var a2 = new AndNode();
            var a3 = new AndNode();
            var a4 = new AndNode();

            a1.Step(NodeCurrent.Low);
            a1.Step(NodeCurrent.Low);

            a2.Step(NodeCurrent.High);
            a2.Step(NodeCurrent.Low);

            a3.Step(NodeCurrent.Low);
            a3.Step(NodeCurrent.High);

            a4.Step(NodeCurrent.High);
            a4.Step(NodeCurrent.High);

            Assert.IsTrue(a1.value == NodeCurrent.Low);
            Assert.IsTrue(a2.value == NodeCurrent.Low);
            Assert.IsTrue(a3.value == NodeCurrent.Low);
            Assert.IsTrue(a4.value == NodeCurrent.High);
        }

        [TestMethod]
        public void NandNodeOutputTest()
        {
            var a1 = new NandNode();
            var a2 = new NandNode();
            var a3 = new NandNode();
            var a4 = new NandNode();

            a1.Step(NodeCurrent.Low);
            a1.Step(NodeCurrent.Low);

            a2.Step(NodeCurrent.High);
            a2.Step(NodeCurrent.Low);

            a3.Step(NodeCurrent.Low);
            a3.Step(NodeCurrent.High);

            a4.Step(NodeCurrent.High);
            a4.Step(NodeCurrent.High);

            Assert.IsTrue(a1.value == NodeCurrent.High);
            Assert.IsTrue(a2.value == NodeCurrent.High);
            Assert.IsTrue(a3.value == NodeCurrent.High);
            Assert.IsTrue(a4.value == NodeCurrent.Low);
        }

        [TestMethod]
        public void OrNodeOutputTest()
        {
            var a1 = new OrNode();
            var a2 = new OrNode();
            var a3 = new OrNode();
            var a4 = new OrNode();

            a1.Step(NodeCurrent.Low);
            a1.Step(NodeCurrent.Low);

            a2.Step(NodeCurrent.High);
            a2.Step(NodeCurrent.Low);

            a3.Step(NodeCurrent.Low);
            a3.Step(NodeCurrent.High);

            a4.Step(NodeCurrent.High);
            a4.Step(NodeCurrent.High);

            Assert.IsTrue(a1.value == NodeCurrent.Low);
            Assert.IsTrue(a2.value == NodeCurrent.High);
            Assert.IsTrue(a3.value == NodeCurrent.High);
            Assert.IsTrue(a4.value == NodeCurrent.High);
        }

        [TestMethod]
        public void NorNodeOutputTest()
        {
            var a1 = new NorNode();
            var a2 = new NorNode();
            var a3 = new NorNode();
            var a4 = new NorNode();

            a1.Step(NodeCurrent.Low);
            a1.Step(NodeCurrent.Low);

            a2.Step(NodeCurrent.High);
            a2.Step(NodeCurrent.Low);

            a3.Step(NodeCurrent.Low);
            a3.Step(NodeCurrent.High);

            a4.Step(NodeCurrent.High);
            a4.Step(NodeCurrent.High);

            Assert.IsTrue(a1.value == NodeCurrent.High);
            Assert.IsTrue(a2.value == NodeCurrent.Low);
            Assert.IsTrue(a3.value == NodeCurrent.Low);
            Assert.IsTrue(a4.value == NodeCurrent.Low);
        }

        [TestMethod]
        public void XorNodeOutputTest()
        {
            var a1 = new XorNode();
            var a2 = new XorNode();
            var a3 = new XorNode();
            var a4 = new XorNode();

            a1.Step(NodeCurrent.Low);
            a1.Step(NodeCurrent.Low);

            a2.Step(NodeCurrent.High);
            a2.Step(NodeCurrent.Low);

            a3.Step(NodeCurrent.Low);
            a3.Step(NodeCurrent.High);

            a4.Step(NodeCurrent.High);
            a4.Step(NodeCurrent.High);

            Assert.IsTrue(a1.value == NodeCurrent.High);
            Assert.IsTrue(a2.value == NodeCurrent.Low);
            Assert.IsTrue(a3.value == NodeCurrent.Low);
            Assert.IsTrue(a4.value == NodeCurrent.High);
        }

        [TestMethod]
        public void NotNodeOutputTest()
        {
            var a1 = new NotNode();
            var a2 = new NotNode();

            a1.Step(NodeCurrent.Low);
            a2.Step(NodeCurrent.High);

            Assert.IsTrue(a1.value == NodeCurrent.High);
            Assert.IsTrue(a2.value == NodeCurrent.Low);
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
