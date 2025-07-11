using LibertyActivator.Commands;
using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using LibertyActivator.Models;
using LibertyActivator.Models.CliCommands;
using LibertyActivator.Models.Commands;
using LibertyActivator.Services;
using LibertyActivator.ViewModels.Base;
using LibertyActivator.Views.Controls;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibertyActivator.ViewModels
{
	public class ActivateViewModel : ViewModelBase
	{
		public ICommand ShowSettingsCommand { get; set; }
		public ICommand ActivateSystemCommand { get; set; }

		private readonly IContentDialogService _contentDialogService;
		private readonly ConfigurateLicenseKeyControl _configurateLicenseKeyControl;
		private readonly ILicenseKeyService _licenseKeyService;
		private readonly ICmdExecutor _cmdExecutor;
		private LicenseKey _selectedKey;
		public LicenseKey SelectedKey
		{
			get => _selectedKey;
			set => SetProperty(ref _selectedKey, value, nameof(SelectedKey));
		}

		public ActivateViewModel(IContentDialogService contentDialogService,
						   ConfigurateLicenseKeyControl configurateLicenseKeyControl,
						   ILicenseKeyService licenseKeyService,
						   ICmdExecutor cmdExecutor)
		{
			_contentDialogService = contentDialogService;
			_configurateLicenseKeyControl = configurateLicenseKeyControl;
			_licenseKeyService = licenseKeyService;
			_cmdExecutor = cmdExecutor;
			InitializeSelectedKey();
			UpdateSelectedKey();
		}
		protected override void InitializeCommands()
		{
			ShowSettingsCommand = new SafeAsyncRelayCommand(ShowSettingsControl);
			ActivateSystemCommand = new SafeRelayCommand(ActivateSystem);
		}
		private async Task ShowSettingsControl()
		{
			await _contentDialogService.ShowDialogAsync("Настройки", _configurateLicenseKeyControl);
			UpdateSelectedKey();
		}
		private void InitializeSelectedKey()
		{
			var key = _licenseKeyService.GetKeyByName(Properties.Settings.Default.SelectedKeyName);
			KeyProvider.SetLicenseKey(key);
		}
		private void UpdateSelectedKey()
		{
			SelectedKey = KeyProvider.GetLicenseKey();
		}
		private void ActivateSystem()
		{
			_cmdExecutor.ExecuteCommandWithAdministratorPermissions(
				SetProductKeyCliCommand.Create(KeyProvider.GetLicenseKey()),
				new ActivateWindowsCommand(),
				new SetKmsServerCommand()
			);
			MessageHelper.ShowInformation("Активация", "Windows успешно активирована");
		}
	}
}
