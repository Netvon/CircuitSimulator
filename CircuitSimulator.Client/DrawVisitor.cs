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

		public HashSet<string> Drawn { get; set; }
		public DrawVisitor(Graph canvas)
		{
			Canvas = canvas;

			Drawn = new HashSet<string>();
		}

		public void Visit(Circuit circuit)
		{
            circuit.inputNodes.ForEach(node => AddEdges(node));
		}



		void AddEdges(Core.Nodes.Node n)
		{
			n.OutputNodes.ForEach(node =>
			{
				if( !Drawn.Contains($"{n.Name}->{node.Name}") )
				{
					var edge = Canvas.AddEdge(n.Name, node.Name);
					Drawn.Add($"{n.Name}->{node.Name}");

					edge.SourceNode.LabelText = $"{n.GetType().Name}\n{n.Name}";
					edge.TargetNode.LabelText = $"{node.GetType().Name}\n{node.Name}";

					switch (n)
					{
						case Core.Nodes.Node input when input.Value == NodeCurrent.High:
							edge.SourceNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleVioletRed;
							break;

						case Core.Nodes.Node input when input.Value == NodeCurrent.Low:
							edge.SourceNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
							break;
					}

					switch (node)
					{
						case Core.Nodes.Node input when input.Value == NodeCurrent.High:
							edge.TargetNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleVioletRed;
							break;

						case Core.Nodes.Node input when input.Value == NodeCurrent.Low:
							edge.TargetNode.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightBlue;
							break;
					}
				}

				AddEdges(node);
			});
		}
	}
}
