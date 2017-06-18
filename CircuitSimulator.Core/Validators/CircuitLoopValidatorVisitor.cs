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
                if (!LoopNodeTillName(node.OutputNodes))
                {
                    throw new CircuitInvalidException();
                }
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
                        return false;
                    }

                }

                loopCheckList.Add(node.GetHashCode());

                if(!LoopNodeTillName(node.OutputNodes))
                {
                    return false;
                }
            }

            return true;

        }


    }
}
