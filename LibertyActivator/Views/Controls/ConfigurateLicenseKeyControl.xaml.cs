using LibertyActivator.ViewModels;
using System.Windows.Controls;

namespace LibertyActivator.Views.Controls
{
	/// <summary>
	/// Логика взаимодействия для ConfigurateLicenseKeyControl.xaml
	/// </summary>
	public partial class ConfigurateLicenseKeyControl : UserControl
	{
		public ConfigurateLicenseKeyControl(ConfigurateLicenseKeyViewModel configurateLicenseKeyViewModel)
		{
			InitializeComponent();
			this.DataContext = configurateLicenseKeyViewModel;
		}
	}
}
