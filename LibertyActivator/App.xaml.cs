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
			var remoteFileReader = ServiceProvider.GetRequiredService<RemoteFileReader>();

			var path = licenceKeyStorage.GetConfigPath();
			if (File.Exists(path))
			{
				return;
			}

			string licenceKeysRemoteUrl = ConfigurationManager.AppSettings["LicenceKeysRemoteUrl"]
					?? throw new ExceptionWithFriendlyMessage("Ссылка на получение ключей не указана в конфигурации.");

			var remoteKeysContent = await remoteFileReader.ReadAsync(licenceKeysRemoteUrl);

			var keys = JsonDeserializeHelper.DeserializeLicenseKeysFromJson(remoteKeysContent);
			remoteKeysContent = Newtonsoft.Json.JsonConvert.SerializeObject(keys);

			File.WriteAllText(path, remoteKeysContent);
		}
	}
}
