using LibertyActivator.Contracts;
using LibertyActivator.ViewModels;
using System.Windows.Controls;

namespace LibertyActivator.Services
{
	public class ContentDialogService : IContentDialogService
	{
		private readonly ContentDialogViewModel _contentDialogViewModel;

		public ContentDialogService(ContentDialogViewModel contentDialogViewModel)
		{
			_contentDialogViewModel = contentDialogViewModel;
		}

		public void CloseDialog()
		{
			_contentDialogViewModel.Close();
		}

		public void ShowDialog(string title, UserControl contentControl)
		{
			_contentDialogViewModel.Title = title;
			_contentDialogViewModel.CurrentContent = contentControl;
			_contentDialogViewModel.IsShowDialog = true;
		}
	}
}
