using LibertyActivator.Contracts;
using LibertyActivator.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет инструмент взаимодействия с диалогом.
	/// </summary>
	public class ContentDialogService : IContentDialogService
	{
		#region Data
		private readonly ContentDialogViewModel _contentDialogViewModel;
		private TaskCompletionSource<bool> _dialogCompletionSource;
		#endregion

		#region .ctor
		public ContentDialogService(ContentDialogViewModel contentDialogViewModel)
		{
			_contentDialogViewModel = contentDialogViewModel;
			_contentDialogViewModel.DialogClosed += OnDialogClosed;
		}
		#endregion

		#region Public
		/// <inheritdoc/>
		public async Task ShowDialogAsync(string title, UserControl contentControl)
		{
			_dialogCompletionSource = new TaskCompletionSource<bool>();

			_contentDialogViewModel.Title = title;
			_contentDialogViewModel.CurrentContent = contentControl;
			_contentDialogViewModel.IsShowDialog = true;

			await _dialogCompletionSource.Task;
		}

		/// <inheritdoc/>
		public void CloseDialog() => _contentDialogViewModel.Close();
		#endregion

		#region Private
		private void OnDialogClosed(object sender, EventArgs e)
		{
			_dialogCompletionSource?.TrySetResult(true);
		}
		#endregion
	}
}
