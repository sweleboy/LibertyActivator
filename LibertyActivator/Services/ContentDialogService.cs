using LibertyActivator.Contracts;
using LibertyActivator.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibertyActivator.Services
{
	public class ContentDialogService : IContentDialogService
	{
		private readonly ContentDialogViewModel _contentDialogViewModel;
		private TaskCompletionSource<bool> _dialogCompletionSource;

		public ContentDialogService(ContentDialogViewModel contentDialogViewModel)
		{
			_contentDialogViewModel = contentDialogViewModel;
			_contentDialogViewModel.DialogClosed += OnDialogClosed;
		}

		public async Task ShowDialogAsync(string title, UserControl contentControl)
		{
			_dialogCompletionSource = new TaskCompletionSource<bool>();

			_contentDialogViewModel.Title = title;
			_contentDialogViewModel.CurrentContent = contentControl;
			_contentDialogViewModel.IsShowDialog = true;

			await _dialogCompletionSource.Task;
		}

		public void CloseDialog() => _contentDialogViewModel.Close();

		private void OnDialogClosed(object sender, EventArgs e)
		{
			_dialogCompletionSource?.TrySetResult(true);
		}
	}
}
