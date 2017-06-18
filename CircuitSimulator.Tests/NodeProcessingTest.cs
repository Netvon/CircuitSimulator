using System;
using System.Linq;
using CircuitSimulator.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CircuitSimulator.Core.Nodes;

namespace CircuitSimulator.Tests
{
	[TestClass]
	public class NodeProcessingTest
	{

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

            Assert.IsTrue(a1.Value == NodeCurrent.Low);
            Assert.IsTrue(a2.Value == NodeCurrent.Low);
            Assert.IsTrue(a3.Value == NodeCurrent.Low);
            Assert.IsTrue(a4.Value == NodeCurrent.High);
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

            Assert.IsTrue(a1.Value == NodeCurrent.High);
            Assert.IsTrue(a2.Value == NodeCurrent.High);
            Assert.IsTrue(a3.Value == NodeCurrent.High);
            Assert.IsTrue(a4.Value == NodeCurrent.Low);
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

            Assert.IsTrue(a1.Value == NodeCurrent.Low);
            Assert.IsTrue(a2.Value == NodeCurrent.High);
            Assert.IsTrue(a3.Value == NodeCurrent.High);
            Assert.IsTrue(a4.Value == NodeCurrent.High);
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

            Assert.IsTrue(a1.Value == NodeCurrent.High);
            Assert.IsTrue(a2.Value == NodeCurrent.Low);
            Assert.IsTrue(a3.Value == NodeCurrent.Low);
            Assert.IsTrue(a4.Value == NodeCurrent.Low);
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

            Assert.IsTrue(a1.Value == NodeCurrent.High);
            Assert.IsTrue(a2.Value == NodeCurrent.Low);
            Assert.IsTrue(a3.Value == NodeCurrent.Low);
            Assert.IsTrue(a4.Value == NodeCurrent.High);
        }

        [TestMethod]
        public void NotNodeOutputTest()
        {
            var a1 = new NotNode();
            var a2 = new NotNode();

            a1.Step(NodeCurrent.Low);
            a2.Step(NodeCurrent.High);

            Assert.IsTrue(a1.Value == NodeCurrent.High);
            Assert.IsTrue(a2.Value == NodeCurrent.Low);
        }

	}
}
