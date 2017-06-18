using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator.Core.Validators
{
    public interface IVisitor
    {

        void Visit(Circuit circuit);

    }
}
