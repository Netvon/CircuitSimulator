using CircuitSimulator.Core.Nodes;
using CircuitSimulator.Core.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitSimulator.Core
{
	public class CircuitConnectionValidatorVisitor : IVisitor
	{


        // Don't call this validator before the circuit is checked for loops, or it will keep looping...


        public void Visit(Circuit circuit)
        {

            if (circuit == null)
            {
                throw new CircuitInvalidException();
            }


            foreach (var input in circuit.inputNodes)
            {

                if (!loopNodeToOutputs(input))
                {
                    throw new CircuitInvalidException();
                }

            }

        }


        private bool loopNodeToOutputs(Node node)
        {

            if (node.GetType() == typeof(OutputNode))
            {
                // Output reached
                return true;
            }

            if (node.OutputNodes.Count != 0)
            {
                foreach (var output in node.OutputNodes)
                {
                    if (!this.loopNodeToOutputs(output))
                    {
                        // One node doesn't have an output
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
