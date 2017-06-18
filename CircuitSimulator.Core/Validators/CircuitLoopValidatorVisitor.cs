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

                this.loopCheckList.Add(node.GetHashCode());
                if (!this.loopNodeTillName(node.OutputNodes))
                {
                    throw new CircuitInvalidException();
                }
                this.loopCheckList.Clear();

            }

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


    }
}
