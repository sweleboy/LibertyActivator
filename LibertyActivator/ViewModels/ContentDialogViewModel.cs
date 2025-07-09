using LibertyActivator.Commands;
using LibertyActivator.Contracts;
using LibertyActivator.ViewModels.Base;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibertyActivator.ViewModels
{
	public class ContentDialogViewModel : ViewModelBase
	{
		public ICommand CloseDialogCommand { get; set; }

		private bool _isShowDialog;
		public bool IsShowDialog
		{
			get => _isShowDialog;
			set => SetProperty(ref _isShowDialog, value, nameof(IsShowDialog));
		}
		private string _title = string.Empty;
		public string Title
		{
			get => _title;
			set => SetProperty(ref _title, value, nameof(Title));
		}

		private UserControl _currentContent;

		public UserControl CurrentContent
		{
			get => _currentContent;
			set => SetProperty(ref _currentContent, value, nameof(CurrentContent));
		}

		protected override void InitializeCommands()
		{
			CloseDialogCommand = new SafeRelayCommand(Close);
		}
		public void Close()
		{
			Title = string.Empty;
			IsShowDialog = false;
			CurrentContent = null;
		}
	}
}
