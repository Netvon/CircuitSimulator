using System;

namespace CircuitSimulator.Core
{
	public class CircuitInvalidException : Exception
	{
		public int Line { get; set; }

		public CircuitInvalidException()
		{
		}

		public CircuitInvalidException(string message) : base(message)
		{
		}

		public CircuitInvalidException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}