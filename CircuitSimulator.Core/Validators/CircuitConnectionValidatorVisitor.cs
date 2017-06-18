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


            foreach (var input in circuit.nodes)
            {

                loopNodeToOutputs(input);
                
            }

        }


        private void loopNodeToOutputs(Node node)
        {

            if (node.GetType() == typeof(OutputNode))
            {
                // Output reached
                return;
            }

            if (node.OutputNodes.Count != 0)
            {
                foreach (var output in node.OutputNodes)
                {
                    this.loopNodeToOutputs(output);
                }

            }
            else
            {
                throw new CircuitInvalidException();
            }
        }

    }
}
