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
		public ICommand ShowSettingsCommand { get; set; }

		private readonly IContentDialogService _contentDialogService;
		private readonly ConfigurateLicenseKeyControl _configurateLicenseKeyControl;
		private LicenseKey _selectedKey;
		public LicenseKey SelectedKey
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
			ShowSettingsCommand = new SafeRelayCommand(ShowSettingsControl);
		}
		private void ShowSettingsControl()
		{
			_contentDialogService.ShowDialog("Настройки", _configurateLicenseKeyControl);
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
