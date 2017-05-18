using System;

namespace CircuitSimulator.Core
{
	internal class CircuitParserException : Exception
	{
		public int Line { get; set; }

		public CircuitParserException()
		{
		}

		public CircuitParserException(string message) : base(message)
		{
		}

		public CircuitParserException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}