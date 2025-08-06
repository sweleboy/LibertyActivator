using LibertyActivator.Events;
using LibertyActivator.ViewModels.Base;
using Prism.Events;

namespace LibertyActivator.ViewModels
{
	/// <summary>
	/// Представляет модель представляение для содержимого кнопки активации.
	/// </summary>
	public class ActivateButtonContentViewModel : ViewModelBase
	{
		#region Data
		private readonly IEventAggregator _eventAggregator;

		private bool _isActivating = false;
		public bool IsActivating
		{
			get => _isActivating;
			set => SetProperty(ref _isActivating, value);
		}
		#endregion

		#region .ctor
		public ActivateButtonContentViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			AddActivationEventSubscribe();
		}
		#endregion

		#region Private 
		/// <summary>
		/// Добавляет подписку на событие активации.
		/// </summary>
		private void AddActivationEventSubscribe()
		{
			_eventAggregator.GetEvent<ActivateSystemEvent>()
					   .Subscribe(isActive => IsActivating = isActive);
		}
		#endregion
	}
}
