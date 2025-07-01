using LibertyActivator.Contracts;
using LibertyActivator.ViewModels.Base;

namespace LibertyActivator.ViewModels
{
	public class ActivateViewModel : ViewModelBase
	{
		private readonly IContentDialogService _contentDialogService;

		public ActivateViewModel(IContentDialogService contentDialogService)
		{
			_contentDialogService = contentDialogService;
		}
	}
}
