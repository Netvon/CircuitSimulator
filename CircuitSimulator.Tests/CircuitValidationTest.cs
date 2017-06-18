using System;
using System.Linq;
using CircuitSimulator.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CircuitSimulator.Core.Nodes;

namespace CircuitSimulator.Tests
{
	[TestClass]
	public class CircuitValidationTest
	{

        [TestMethod]
        public void CircuitValidatesConnected()
        {
            // Later change to importing the circuit from file
            var c = new Circuit();
            var i1 = new InputNode();
            var i2 = new InputNode();
            var n1 = new AndNode();
            var o1 = new OutputNode();

            c.AddInput(i1, NodeCurrent.None);
            c.AddInput(i2, NodeCurrent.None);

            i1.AddOutput(n1);
            i2.AddOutput(n1);
            n1.AddOutput(o1);

            c.Add(n1);


            c.Accept(new CircuitConnectionValidatorVisitor());

            Assert.IsTrue(true);

        }

        [TestMethod]
        public void CircuitValidatesNotConnectedToOutput()
        {
            // Later change to importing the circuit from file
            var c = new Circuit();
            var i1 = new InputNode();
            var i2 = new InputNode();
            var n1 = new AndNode();

            c.AddInput(i1, NodeCurrent.None);
            c.AddInput(i2, NodeCurrent.None);

            i1.AddOutput(n1);
            i2.AddOutput(n1);

            c.Add(n1);


            Assert.ThrowsException<CircuitInvalidException>(() =>
            {
                c.Accept(new CircuitConnectionValidatorVisitor());
            });

        }

        [TestMethod]
        public void CircuitValidatesLooped()
        {
            // Later change to importing the circuit from file
            var c = new Circuit();
            var i1 = new InputNode();
            
            var n1 = new NotNode();
            var n2 = new NotNode();
            var n3 = new NotNode();

            c.AddInput(i1, NodeCurrent.None);
            c.Add(i1);
            c.Add(n1);
            c.Add(n2);
            c.Add(n3);

            i1.AddOutput(n1);
            n1.AddOutput(n2);
            n2.AddOutput(n3);
            n3.AddOutput(n1);


            Assert.ThrowsException<CircuitInvalidException>(() =>
            {
                c.Accept(new CircuitLoopValidatorVisitor());
            });

        }

    }
}
