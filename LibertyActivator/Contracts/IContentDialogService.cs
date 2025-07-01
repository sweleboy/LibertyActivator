using System.Windows.Controls;

namespace LibertyActivator.Contracts
{
	public interface IContentDialogService
	{
		void ShowDialog(UserControl contentControl);
		void CloseDialog();
	}
}
