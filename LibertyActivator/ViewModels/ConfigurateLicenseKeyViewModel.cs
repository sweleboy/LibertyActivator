using LibertyActivator.Commands;
using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using LibertyActivator.Models;
using LibertyActivator.Services;
using LibertyActivator.ViewModels.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LibertyActivator.ViewModels
{
    public class ConfigurateLicenseKeyViewModel : ViewModelBase
    {
        public ICommand SaveSelectedLicenseKeyCommand { get; set; }
        public ICommand OpenKeysFileCommand { get; set; }

        private readonly ILicenseKeysStorage _licenseKeysStorage;
        private readonly IContentDialogService _contentDialogService;

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
            OpenKeysFileCommand = new SafeRelayCommand(OpenKeysFile);
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
        private void OpenKeysFile()
        {
            string keysFilePath = _licenseKeysStorage.GetConfigPath();
            if (!File.Exists(keysFilePath))
            {
                bool canCreateKeysFile = MessageHelper.ShowQuestion("Просмотр ключей", "Невозможно открыть файл с лицензионными ключами. Причина: файл не найден\n\nХоти создать?") == MessageBoxResult.Yes;
                if (!canCreateKeysFile)
                {
                    return;
                }

                CreateKeysFile(keysFilePath);
            }

            Process.Start(keysFilePath);
        }
        private void InitializeSelectedKey()
        {
            SelectedKey = Keys.FirstOrDefault(x => x.Name.Equals(Properties.Settings.Default.SelectedKeyName));
        }

        private void CreateKeysFile(string filePath)
        {
            var keys = new List<LicenseKey>();
            keys.Add(new LicenseKey("TestOS", "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"));
            var keysAsJson = JsonConvert.SerializeObject(keys);
            File.WriteAllText(filePath, keysAsJson);
        }
    }
}
