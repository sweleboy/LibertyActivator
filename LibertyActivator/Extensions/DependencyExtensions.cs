using LibertyActivator.Contracts;
using LibertyActivator.Services;
using LibertyActivator.ViewModels;
using LibertyActivator.Views.Controls;
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
			services.AddSingleton<ConfigurateLicenseKeyControl>();
			
			services.AddSingleton<ActivateViewModel>();
			services.AddSingleton<ContentDialogViewModel>();
			services.AddSingleton<ConfigurateLicenseKeyViewModel>();

			services.AddSingleton<IContentDialogService, ContentDialogService>();
			services.AddTransient<ILicenseKeyService, LicenseKeyService>();
			services.AddTransient<ILicenseKeysStorage, LicenseKeysStorage>();
			services.AddTransient<ICommandExecutor, CommandExecutor>();
			services.AddTransient<IProcessBuilder, CmdProcessBuilder>();
			services.AddTransient<IProcessExecutor, ProcessExecutor>();

			return services;
		}
	}
}
