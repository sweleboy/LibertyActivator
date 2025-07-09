using LibertyActivator.Commands;
using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using LibertyActivator.Models;
using LibertyActivator.Services;
using LibertyActivator.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LibertyActivator.ViewModels
{
	public class ConfigurateLicenseKeyViewModel : ViewModelBase
	{
		private readonly ILicenseKeysStorage _licenseKeysStorage;
		private readonly IContentDialogService _contentDialogService;

		public ICommand SaveSelectedLicenseKeyCommand { get; set; }

		private ObservableCollection<LicenseKey> _keys = new ObservableCollection<LicenseKey>();
		public ObservableCollection<LicenseKey> Keys
		{
			get => _keys;
			set => SetProperty(ref _keys, value, nameof(Keys));
		}

		private LicenseKey _selectedKey;
		public LicenseKey SelectedKey
		{
			get => _selectedKey;
			set => SetProperty(ref _selectedKey, value, nameof(SelectedKey));
		}

		public ConfigurateLicenseKeyViewModel(ILicenseKeysStorage licenseKeysStorage, IContentDialogService contentDialogService)
		{
			_licenseKeysStorage = licenseKeysStorage;
			_contentDialogService = contentDialogService;
			LoadKeys();
			InitializeSelectedKey();
		}
		protected override void InitializeCommands()
		{
			SaveSelectedLicenseKeyCommand = new SafeRelayCommand(SaveSelectedLicenseKey);
		}
		private void SaveSelectedLicenseKey()
		{
			if (SelectedKey == null)
			{
				MessageHelper.ShowError("Ошибка", "Не возможно сохранить лицензионный ключ. Причина: лицензионный ключ не выбран");
				return;
			}

			KeyProvider.SetLicenseKey(SelectedKey);
			Properties.Settings.Default.SelectedKeyName = SelectedKey.Name;
			Properties.Settings.Default.Save();
			_contentDialogService.CloseDialog();
		}
		private void LoadKeys()
		{
			Keys = new ObservableCollection<LicenseKey>(_licenseKeysStorage.GetKeys());
		}
		private void InitializeSelectedKey()
		{
			SelectedKey = Keys.FirstOrDefault(x => x.Name.Equals(Properties.Settings.Default.SelectedKeyName));
		}
	}
}
