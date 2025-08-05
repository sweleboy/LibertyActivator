using LibertyActivator.Commands;
using LibertyActivator.Constants;
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
	/// <summary>
	/// Представляет модель представления для активации.
	/// </summary>
	public class ActivateViewModel : ViewModelBase
	{
		#region Data
		public ICommand ShowSettingsCommand { get; set; }
		public ICommand ActivateSystemCommand { get; set; }

		private readonly IContentDialogService _contentDialogService;
		private readonly ConfigurateLicenseKeyControl _configurateLicenseKeyControl;
		private readonly ILicenseKeyService _licenseKeyService;
		private readonly ICommandExecutor _cmdExecutor;
		private LicenseKey _selectedKey;
		public LicenseKey SelectedKey
		{
			get => _selectedKey;
			set => SetProperty(ref _selectedKey, value, nameof(SelectedKey));
		}
		#endregion

		#region .ctor
		public ActivateViewModel(IContentDialogService contentDialogService,
						   ConfigurateLicenseKeyControl configurateLicenseKeyControl,
						   ILicenseKeyService licenseKeyService,
						   ICommandExecutor cmdExecutor)
		{
			_contentDialogService = contentDialogService;
			_configurateLicenseKeyControl = configurateLicenseKeyControl;
			_licenseKeyService = licenseKeyService;
			_cmdExecutor = cmdExecutor;
			InitializeSelectedKey();
			UpdateSelectedKey();
		}
		#endregion

		#region Protected
		protected override void InitializeCommands()
		{
			ShowSettingsCommand = new SafeAsyncRelayCommand(ShowSettingsControlAsync);
			ActivateSystemCommand = new SafeAsyncRelayCommand(ActivateSystemAsync);
		}
		#endregion

		#region Private
		/// <summary>
		/// Показывает контролл настройки-конфигурацию.
		/// </summary>
		private async Task ShowSettingsControlAsync()
		{
			await _contentDialogService.ShowDialogAsync("Настройки", _configurateLicenseKeyControl);
			UpdateSelectedKey();
		}

		/// <summary>
		/// Инициализирует выбранный ключ.
		/// </summary>
		private void InitializeSelectedKey()
		{
			var key = _licenseKeyService.GetKeyByName(Properties.Settings.Default.SelectedKeyName);
			KeyProvider.SetLicenseKey(key);
		}

		/// <summary>
		/// Обновляет выбранный ключ.
		/// </summary>
		private void UpdateSelectedKey()
		{
			SelectedKey = KeyProvider.GetLicenseKey();
		}
		/// <summary>
		/// Активирует систему.
		/// </summary>
		private async Task ActivateSystemAsync()
		{
			int activateResultCode = await _cmdExecutor.ExecuteCommandsWithAdministratorPermissionsAsync(
				SetProductKeyCliCommand.Create(KeyProvider.GetLicenseKey()),
				new ActivateWindowsCommand(),
				new SetKmsServerCommand()
			);
			if (activateResultCode != ExitCodes.SuccessExitCode)
			{
				MessageHelper.ShowError("Ошибка активации", "Произошла ошибка при активации системы. Попробуйте перезапустить приложение с правами администратора.");
				return;
			}

			MessageHelper.ShowInformation("Активация", "Windows успешно активирована");
		}
		#endregion
	}
}
