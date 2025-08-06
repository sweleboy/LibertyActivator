using LibertyActivator.Contracts;
using LibertyActivator.Events;
using LibertyActivator.Helpers;
using Prism.Events;
using System;

namespace LibertyActivator.Services
{
	public class ExceptionHandler : IExceptionHandler
	{
		private readonly IEventAggregator _eventAggregator;

		public ExceptionHandler(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}
		public void Handle(Exception exception)
		{
			_eventAggregator.GetEvent<ActivateSystemEvent>().Publish(false);
			MessageHelper.ShowError("Ошибка", $"{exception.Message}");
		}
	}
}
