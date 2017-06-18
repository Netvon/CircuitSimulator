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

namespace CircuitSimulator.Client
{
	class DrawVisitor : IVisitor
	{
		public Canvas Canvas { get; set; }

		public Dictionary<Node, Grid> Drawn { get; set; }
		public DrawVisitor(Canvas canvas)
		{
			Canvas = canvas;

			Drawn = new Dictionary<Node, Grid>();
		}

		public void Visit(Circuit circuit)
		{
			int level = 0;
			circuit.inputNodes.ForEach(node =>
			{
				DrawNode(circuit.inputNodes.Select(x => x as Node).ToList(), node, level);

				node.OutputNodes.ForEach(nodeLvl2 =>
				{
					DrawNode(node.OutputNodes, nodeLvl2, level + 1);
				});
			});
		}

		private void DrawNode(List<Node> source, Node node, int level)
		{
			if (Drawn.ContainsKey(node))
				return;

			Grid g = new Grid()
			{
				Width = 32,
				Height = 32
			};

			Label label = new Label()
			{
				Content = node.Name
			};

			if (node.Value == NodeCurrent.Low)
			{
				label.Background = Brushes.LightBlue;
			}
			else if (node.Value == NodeCurrent.High)
			{
				label.Background = Brushes.PaleVioletRed;
			} else
			{
				label.Background = Brushes.LightGray;
			}

			g.Children.Add(label);

			int index = source.IndexOf(node);
			Canvas.SetTop(g, 32 * index + (16 * index));
			Canvas.SetLeft(g, 32 * level + (32 * level));

			Canvas.Children.Add(g);
			Drawn.Add(node, g);
		}
	}
}
