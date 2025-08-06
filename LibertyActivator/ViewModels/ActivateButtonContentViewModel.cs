using LibertyActivator.Events;
using LibertyActivator.ViewModels.Base;
using Prism.Events;

namespace LibertyActivator.ViewModels
{
	public class ActivateButtonContentViewModel : ViewModelBase
	{
		private readonly IEventAggregator _eventAggregator;

		private bool _isActivating = false;
		public bool IsActivating
		{
			get => _isActivating;
			set => SetProperty(ref _isActivating, value);
		}

		public ActivateButtonContentViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.GetEvent<ActivateSystemEvent>()
					   .Subscribe(isActive => IsActivating = isActive);
		}
	}
}
