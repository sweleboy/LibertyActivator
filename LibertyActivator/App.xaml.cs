using LibertyActivator.Contracts;
using LibertyActivator.Exceptions;
using LibertyActivator.Extensions;
using LibertyActivator.Helpers;
using LibertyActivator.Services;
using LibertyActivator.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace LibertyActivator
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{

#pragma warning disable
		public IServiceProvider ServiceProvider { get; private set; }
#pragma warning enable
		/// <inheritdoc/>
		public App()
		{
			var serviceCollection = new ServiceCollection();

			serviceCollection.AddApplicationServices();

			ServiceProvider = serviceCollection.BuildServiceProvider();

			DispatcherUnhandledException += App_DispatcherUnhandledException;
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
			TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
		}

		private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
		{
			ServiceProvider.GetRequiredService<IExceptionHandler>().Handle(e.Exception);
			e.SetObserved();
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception = e.ExceptionObject as Exception;
			ServiceProvider.GetRequiredService<IExceptionHandler>().Handle(exception);
		}

		private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			ServiceProvider.GetRequiredService<IExceptionHandler>().Handle(e.Exception);
			e.Handled = true;
		}
		/// <inheritdoc/>
		protected override async void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			await LoadKeysAsync();

			var activateWindow = ServiceProvider.GetRequiredService<ActivateWindow>();
			activateWindow.Show();
		}

		private async Task LoadKeysAsync()
		{
			var licenceKeyStorage = ServiceProvider.GetRequiredService<ILicenseKeysStorage>();
			var licenseKeyService = ServiceProvider.GetRequiredService<ILicenseKeyService>();

			var keys = await licenseKeyService.DownloadKeysFromRemoteSourceAsync();
			await licenseKeyService.SaveKeysToFileAsync(licenceKeyStorage.GetConfigPath(), keys);
		}
	}
}
