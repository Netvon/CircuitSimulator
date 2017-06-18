using CircuitSimulator.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircuitSimulator.Core;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using CircuitSimulator.Core.Nodes;
using Microsoft.Msagl.Drawing;

namespace CircuitSimulator.Client
{
	class DrawVisitor : IVisitor
	{
		public Graph Canvas { get; set; }

		public Dictionary<Core.Nodes.Node, Microsoft.Msagl.Drawing.Node> Drawn { get; set; }
		public DrawVisitor(Graph canvas)
		{
			Canvas = canvas;

			Drawn = new Dictionary<Core.Nodes.Node, Microsoft.Msagl.Drawing.Node>();
		}

		public void Visit(Circuit circuit)
		{
			circuit.inputNodes.ForEach(node => AddEdges(node));
		}

		void AddEdges(Core.Nodes.Node n)
		{
			n.OutputNodes.ForEach(node =>
			{
				Canvas.AddEdge(n.Name, node.Name);

				AddEdges(node);
			});
		}
	}
}
