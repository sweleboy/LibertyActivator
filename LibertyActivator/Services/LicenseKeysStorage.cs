using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using LibertyActivator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibertyActivator.Services
{
	public class LicenseKeysStorage : ILicenseKeysStorage
	{
		private const string ConfigDirectory = "Configs";
		private const string ConfigFileName = "keys.json";
		private readonly string _configPath;
		public LicenseKeysStorage()
		{
			_configPath = GetConfigPath();
		}
		public IReadOnlyCollection<LicenseKey> GetKeys()
		{
			if (!File.Exists(_configPath))
			{
				MessageHelper.ShowError("Чтение из файла", "Произошла ошибка при чтении файла. Причина: файл не найден.");
				return Array.Empty<LicenseKey>();
			}

			var keysAsJson = File.ReadAllText(_configPath);
			return JsonConvert.DeserializeObject<IReadOnlyCollection<LicenseKey>>(keysAsJson);
		}

		private string GetConfigPath()
		{
			var appDir = AppDomain.CurrentDomain.BaseDirectory;

			var configDirectory = Path.Combine(appDir, ConfigDirectory);

			if (!Directory.Exists(configDirectory))
			{
				Directory.CreateDirectory(configDirectory);
			}

			return Path.Combine(configDirectory, ConfigFileName);
		}
	}
}
