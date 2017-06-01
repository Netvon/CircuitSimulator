using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitSimulator.Core.Nodes
{
    public abstract class Node
	{
		public string Name { get; set; }

		public Node OutputNode { get; set; }

		int? value = null;


		public int? Value
		{
			get => value;

			set
			{
				if(value.HasValue)
					this.value = Math.Max(0, Math.Min(1, value.Value));
			}
		}

		public virtual void Step()
		{
			if(OutputNode != null)
			{
				OutputNode.Value = Value;
				OutputNode.Step();
			}
		}
	}
}
