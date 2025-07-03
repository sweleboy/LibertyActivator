using LibertyActivator.Commands;
using LibertyActivator.Contracts;
using LibertyActivator.Models;
using LibertyActivator.Services;
using LibertyActivator.ViewModels.Base;
using LibertyActivator.Views.Controls;
using System.Windows.Input;

namespace LibertyActivator.ViewModels
{
	public class ActivateViewModel : ViewModelBase
	{
		public ICommand ShowConfigurateLicenseKeyControlCommand { get; set; }

		private readonly IContentDialogService _contentDialogService;
		private readonly ConfigurateLicenseKeyControl _configurateLicenseKeyControl;
		private LicenseKey _selectedKey;
		private LicenseKey SelectedKey
		{
			get => _selectedKey;
			set => SetProperty(ref _selectedKey, value, nameof(SelectedKey));
		}

		public ActivateViewModel(IContentDialogService contentDialogService, ConfigurateLicenseKeyControl configurateLicenseKeyControl)
		{
			_contentDialogService = contentDialogService;
			_configurateLicenseKeyControl = configurateLicenseKeyControl;
			InitializeSelectedKey();
			UpdateSelectedKey();
		}
		protected override void InitializeCommands()
		{
			ShowConfigurateLicenseKeyControlCommand = new SafeRelayCommand(ShowConfigurateLicenseKeyControl);
		}
		private void ShowConfigurateLicenseKeyControl()
		{
			_contentDialogService.ShowDialog(_configurateLicenseKeyControl);
			UpdateSelectedKey();
		}
		private void InitializeSelectedKey()
		{
			KeyProvider.SetLicenseKey(
				new LicenseKey(Properties.Settings.Default.SettingsKey,
				//TODO: Добавить извлечение выбранного ключа из настроек, заполнить его в провайдере, через GetKeyByName
				string.Empty)
			);
		}
		private void UpdateSelectedKey()
		{
			SelectedKey = KeyProvider.GetLicenseKey();
		}
	}
}
