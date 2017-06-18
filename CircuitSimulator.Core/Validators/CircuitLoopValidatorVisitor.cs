using CircuitSimulator.Core.Nodes;
using CircuitSimulator.Core.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitSimulator.Core
{
	public class CircuitLoopValidatorVisitor : IVisitor
	{


        List<int> loopCheckList = new List<int>();



        public void Visit(Circuit circuit)
        {

            if (circuit == null)
            {
                throw new CircuitInvalidException();
            }


            foreach (var node in circuit.nodes)
            {

                loopCheckList.Add(node.GetHashCode());
                LoopNodeTillName(node.OutputNodes);
                
                loopCheckList.Clear();

            }

        }

        private bool LoopNodeTillName(List<Node> nodes)
        {



            foreach (var node in nodes)
            {

                foreach (var code in loopCheckList)
                {
                    if(node.GetHashCode() == code)
                    {
                        throw new CircuitInvalidException();
                    }

                }

                loopCheckList.Add(node.GetHashCode());

                LoopNodeTillName(node.OutputNodes);
                loopCheckList.Clear();


            }

            return true;

        }


    }
}
