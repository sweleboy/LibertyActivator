using System;

namespace LibertyActivator.Exceptions
{
	/// <summary>
	/// Представляет ошибку и понятным и дружелбным для пользователя сообщением.
	/// </summary>
	public class ExceptionWithFriendlyMessage : Exception
	{
		public ExceptionWithFriendlyMessage(string message, Exception innerException = null)
			: base(message, innerException)
		{ }
	}
}
