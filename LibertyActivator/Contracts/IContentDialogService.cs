using System.Windows.Controls;

namespace LibertyActivator.Contracts
{
	public interface IContentDialogService
	{
		void ShowDialog(string title, UserControl contentControl);
		void CloseDialog();
	}
}
