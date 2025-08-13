using LibertyActivator.Commands;
using LibertyActivator.Contracts;
using LibertyActivator.Exceptions;
using LibertyActivator.Helpers;
using LibertyActivator.Models;
using LibertyActivator.Services;
using LibertyActivator.ViewModels.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibertyActivator.ViewModels
{
	/// <summary>
	/// Представляет модель представляения для конфигурации лицензионных ключей.
	/// </summary>
	public class ConfigurateLicenseKeyViewModel : ViewModelBase
	{
		#region Data
		public ICommand SaveSelectedLicenseKeyCommand { get; set; }
		public ICommand ResetKeysConfigurationCommand { get; set; }
		public ICommand OpenKeysFileCommand { get; set; }

		private readonly ILicenseKeysStorage _licenseKeysStorage;
		private readonly IContentDialogService _contentDialogService;
		private readonly RemoteFileReader _remoteFileReader;
		private readonly ILicenseKeyService _licenseKeyService;
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
		#endregion

		#region .ctor
		public ConfigurateLicenseKeyViewModel(ILicenseKeysStorage licenseKeysStorage,
			IContentDialogService contentDialogService,
			RemoteFileReader remoteFileReader,
			ILicenseKeyService licenseKeyService)
		{
			_licenseKeysStorage = licenseKeysStorage;
			_contentDialogService = contentDialogService;
			_remoteFileReader = remoteFileReader;
			_licenseKeyService = licenseKeyService;
		}
		#endregion

		#region Ovveride
		protected override void InitializeCommands()
		{
			SaveSelectedLicenseKeyCommand = new SafeRelayCommand(SaveSelectedLicenseKey);
			ResetKeysConfigurationCommand = new SafeAsyncRelayCommand(ResetKeysConfigurationAsync);
			OpenKeysFileCommand = new SafeRelayCommand(OpenKeysFile);
		}

		public override Task InitializeAsync()
		{
			UpdateKeys();

			return base.InitializeAsync();
		}
		#endregion

		#region Private
		/// <summary>
		/// Сохраняет выбранный лицензионный ключ.
		/// </summary>
		private void SaveSelectedLicenseKey()
		{
			if (SelectedKey == null)
			{
				throw new ExceptionWithFriendlyMessage("Не возможно сохранить лицензионный ключ. Причина: лицензионный ключ не выбран");
			}

			KeyProvider.SetLicenseKey(SelectedKey);
			Properties.Settings.Default.SelectedKeyName = SelectedKey.Name;
			Properties.Settings.Default.Save();
			_contentDialogService.CloseDialog();
		}
		/// <summary>
		/// Сбрасывает конфигурацию ключей.
		/// </summary>
		private async Task ResetKeysConfigurationAsync()
		{
			var keys = await _licenseKeyService.DownloadKeysFromRemoteSourceAsync();
			await _licenseKeyService.SaveKeysToFileAsync(_licenseKeysStorage.GetConfigPath(), keys);

			LoadKeys();
			SelectedKey = null;
			KeyProvider.SetLicenseKey(null);
			Properties.Settings.Default.SelectedKeyName = string.Empty;
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// Загружает лицензионные ключи.
		/// </summary>
		private void LoadKeys()
		{
			Keys = new ObservableCollection<LicenseKey>(_licenseKeysStorage.GetKeys());
		}
		/// <summary>
		/// Открывает файл с ключами.
		/// </summary>
		private void OpenKeysFile()
		{
			string keysFilePath = _licenseKeysStorage.GetConfigPath();
			if (!File.Exists(keysFilePath))
			{
				bool canCreateKeysFile = MessageHelper.ShowQuestion("Просмотр ключей", "Невозможно открыть файл с лицензионными ключами. Причина: файл не найден\n\nХотите создать?") == MessageBoxResult.Yes;
				if (!canCreateKeysFile)
				{
					return;
				}

				CreateDefaultKeysFile(keysFilePath);
			}

			OpenFileWithWait(keysFilePath);
			UpdateKeys();
		}

		/// <summary>
		/// Инициализирует выбранный ключ.
		/// </summary>
		private void InitializeSelectedKey()
		{
			var selectedKey = Keys.FirstOrDefault(x => x.Name.Equals(Properties.Settings.Default.SelectedKeyName));
			KeyProvider.SetLicenseKey(selectedKey);
			SelectedKey = selectedKey;
		}

		/// <summary>
		/// Открывает файл с ожиданием его закрытия.
		/// </summary>
		/// <param name="filePath">Путь до файла.</param>
		private void OpenFileWithWait(string filePath)
		{
			using (Process process = new Process())
			{
				process.StartInfo = new ProcessStartInfo
				{
					FileName = "explorer.exe",
					Arguments = $"{Path.GetDirectoryName(filePath)}",
					UseShellExecute = false,
					CreateNoWindow = true
				};

				process.Start();
				process.WaitForExit();
			}
		}

		/// <summary>
		/// Обновляет ключи.
		/// </summary>
		private void UpdateKeys()
		{
			LoadKeys();
			InitializeSelectedKey();
		}

		/// <summary>
		/// Создаёт файл с ключами по умолчанию.
		/// </summary>
		/// <param name="filePath">Путь до файла.</param>
		private void CreateDefaultKeysFile(string filePath)
		{
			var keys = new List<LicenseKey>();
			keys.Add(new LicenseKey("TestOS", "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX"));
			var keysAsJson = JsonConvert.SerializeObject(keys);
			File.WriteAllText(filePath, keysAsJson);
		}
		#endregion
	}
}
