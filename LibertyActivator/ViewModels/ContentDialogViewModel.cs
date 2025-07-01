using LibertyActivator.ViewModels.Base;
using System.Windows.Controls;

namespace LibertyActivator.ViewModels
{
	public class ContentDialogViewModel : ViewModelBase
	{
		private bool _isShowDialog;
		public bool IsShowDialog
		{
			get => _isShowDialog;
			set => SetProperty(ref _isShowDialog, value, nameof(IsShowDialog));
		}

		private UserControl _currentContent;
		public UserControl CurrentContent
		{
			get => _currentContent;
			set => SetProperty(ref _currentContent, value, nameof(CurrentContent));
		}
	}
}
