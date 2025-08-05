using System.Windows;

namespace LibertyActivator.Helpers
{
	/// <summary>
	/// Представляет помощника для работы с сообщениями.
	/// </summary>
	public static class MessageHelper
	{
		/// <summary>
		/// Показывает информационное сообщение.
		/// </summary>
		/// <param name="title">Заголовок сообщения.</param>
		/// <param name="message">Текст сообщения.</param>
		/// <returns>Результат сообщения(диалога).</returns>
		public static MessageBoxResult ShowInformation(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);

		/// <summary>
		/// Показывает сообщение ошибки.
		/// </summary>
		/// <param name="title">Заголовок сообщения.</param>
		/// <param name="message">Текст сообщения.</param>
		/// <returns>Результат сообщения(диалога).</returns>
		public static MessageBoxResult ShowError(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);

		/// <summary>
		/// Показывает сообщение с вопросом.
		/// </summary>
		/// <param name="title">Заголовок сообщения.</param>
		/// <param name="message">Текст сообщения.</param>
		/// <returns>Результат сообщения(диалога).</returns>
		public static MessageBoxResult ShowQuestion(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
	}
}
