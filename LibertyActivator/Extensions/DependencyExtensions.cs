using LibertyActivator.Views.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace LibertyActivator.Extensions
{
	/// <summary>
	/// Представляет методы расширения для <see cref="IServiceCollection"/>
	/// </summary>
	public static class DependencyExtensions
	{
		/// <summary>
		/// Добавляет сервисы в коллекцию сервисов приложения.
		/// </summary>
		/// <param name="services">Коллекция сервисов.</param>
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddSingleton<ActivateWindow>();

			return services;
		}
	}
}
