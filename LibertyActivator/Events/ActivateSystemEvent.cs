using Prism.Events;

namespace LibertyActivator.Events
{
	/// <summary>
	/// Представляет событие активации системы.
	/// </summary>
	public class ActivateSystemEvent : PubSubEvent<bool>
	{
	}
}
