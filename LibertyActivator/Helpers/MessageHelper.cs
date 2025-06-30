using System.Windows;

namespace LibertyActivator.Helpers
{
	public static class MessageHelper
	{
		public static MessageBoxResult ShowInformation(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
		public static MessageBoxResult ShowError(string title, string message) => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
	}
}
