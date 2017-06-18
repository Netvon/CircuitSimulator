using CircuitSimulator.Core.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator.Core
{
    public interface IVisitable
    {

        void Accept(IVisitor visitor);
    }
}
