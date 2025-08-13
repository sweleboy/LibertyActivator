using LibertyActivator.ViewModels;
using System.Windows.Controls;

namespace LibertyActivator.Views.Controls
{
	/// <summary>
	/// Логика взаимодействия для ConfigurateLicenseKeyControl.xaml
	/// </summary>
	public partial class ConfigurateLicenseKeyControl : UserControl
	{
		private readonly ConfigurateLicenseKeyViewModel _configurateLicenseKeyViewModel;

		public ConfigurateLicenseKeyControl(ConfigurateLicenseKeyViewModel configurateLicenseKeyViewModel)
		{
			InitializeComponent();
			this.DataContext = configurateLicenseKeyViewModel;
			_configurateLicenseKeyViewModel = configurateLicenseKeyViewModel;
		}

		private async void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			await _configurateLicenseKeyViewModel.InitializeAsync();
		}
	}
}
