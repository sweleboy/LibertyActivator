using LibertyActivator.Extensions;
using LibertyActivator.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using System;
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
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var activateWindow = ServiceProvider.GetRequiredService<ActivateWindow>();
			activateWindow.Show();
		}
	}
}
