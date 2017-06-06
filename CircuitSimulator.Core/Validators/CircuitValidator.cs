using CircuitSimulator.Core.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitSimulator.Core
{
	public class CircuitValidator
	{


        List<int> loopCheckList = new List<int>();


        // Don't call this function before the circuit is checked for loops
        public bool allNodesConnectedToOutput(Circuit circuit)
        {
            if (circuit == null)
            {
                return false;
            }


            foreach (var input in circuit.inputNodes)
            {

                if (!loopNodeToOutputs(input))
                {
                    return false;
                }

            }

            return true;
        }

        public bool notLooping(Circuit circuit)
        {

            if (circuit == null)
            {
                return false;
            }


            foreach (var node in circuit.nodes)
            {

                this.loopCheckList.Add(node.GetHashCode());
                if (!this.loopNodeTillName(node.OutputNodes))
                {
                    return false;
                }
                this.loopCheckList.Clear();
                
            }


            return true;
        }



        private bool loopNodeTillName(List<Node> nodes)
        {

            foreach (var node in nodes)
            {

                foreach (var code in this.loopCheckList)
                {
                    if(node.GetHashCode() == code)
                    {
                        return false;
                    }

                }

                this.loopCheckList.Add(node.GetHashCode());

                if(!loopNodeTillName(node.OutputNodes))
                {
                    return false;
                }
            }

            return true;

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
