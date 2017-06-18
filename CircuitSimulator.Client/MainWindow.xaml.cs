using CircuitSimulator.Core;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CircuitSimulator.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		public MainWindow()
		{
			InitializeComponent();

			Viewer = new GViewer()
			{
				EdgeInsertButtonVisible = false,
				NavigationVisible = false,
				LayoutEditingEnabled = false,
				LayoutAlgorithmSettingsButtonVisible = false,
				InsertingEdge = false,
				UndoRedoButtonsVisible = false,
				ToolBarIsVisible = false,
				BackColor = System.Drawing.Color.White
			};

			FormsHost.Child = Viewer;
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			await Run();
		}

		public Circuit Circuit { get; set; }
		public GViewer Viewer { get; set; }

		private async Task Run()
		{
			
			ErrorLabel.Content = "";

			var ofd = new OpenFileDialog();

			if (ofd.ShowDialog() == true)
			{
				try
				{
					Inputs.Children.Clear();

					var builder = new CircuitBuilder();
					Circuit = await builder.AddDefaultNodes()
							.AddFileSource(ofd.FileName)
							.Build();

					Circuit.Accept(new CircuitLoopValidatorVisitor());
					Circuit.Accept(new CircuitConnectionValidatorVisitor());

					Circuit.inputNodes.ForEach(inp =>
					{
						CheckBox checkBox = new CheckBox()
						{
							Content = inp.Name,
							IsChecked = inp.Value == Core.Nodes.NodeCurrent.High
						};

						checkBox.Checked += (s, e) =>
						{
							inp.Value = Core.Nodes.NodeCurrent.High;
							StartAndDraw();
						};

						checkBox.Unchecked += (s, e) =>
						{
							inp.Value = Core.Nodes.NodeCurrent.Low;
							StartAndDraw();
						};
						Inputs.Children.Add(checkBox);
					});

					StartAndDraw();
				}
				catch (Exception ex)
				{
					ErrorLabel.Content = "Error bij het valideren: " + ex.Message;
				}
			}
		}

		void StartAndDraw()
		{
			Graph graph = new Graph("graph");

			Circuit.Reset();
			Circuit.Start();

			Circuit.Accept(new DrawVisitor(graph));
			Viewer.Graph = graph;
		}
	}
}
