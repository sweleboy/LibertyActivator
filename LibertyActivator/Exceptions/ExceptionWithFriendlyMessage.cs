using System;

namespace LibertyActivator.Exceptions
{
	public class ExceptionWithFriendlyMessage : Exception
	{
		public ExceptionWithFriendlyMessage(string message, Exception innerException = null)
			: base(message, innerException)
		{ }
	}
}
