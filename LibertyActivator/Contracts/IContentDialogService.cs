using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibertyActivator.Contracts
{
	public interface IContentDialogService
	{
		Task ShowDialogAsync(string title, UserControl contentControl);
		void CloseDialog();
	}
}
