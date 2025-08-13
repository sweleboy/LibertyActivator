using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using LibertyActivator.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет инструмент для взаимодействия с хранилищем лицензионных ключей.
	/// </summary>
	public class LicenseKeysStorage : ILicenseKeysStorage
	{
		#region Data
		private const string ConfigFileName = "keys.json";
		private readonly string _configPath;
		private readonly LocalFileReader _localFileReader;
		private readonly RemoteFileReader _remoteFileReader;
		#endregion

		#region .ctor
		public LicenseKeysStorage(LocalFileReader localFileReader, RemoteFileReader remoteFileReader)
		{
			_configPath = BuildConfigPath();
			_localFileReader = localFileReader;
			_remoteFileReader = remoteFileReader;
		}
		#endregion

		#region Public
		/// <inheritdoc/>
		public string GetConfigPath() => _configPath;

		/// <inheritdoc/>
		public IReadOnlyCollection<LicenseKey> GetKeys()
		{
			var keysAsJson = _localFileReader.Read(_configPath);
			return JsonDeserializeHelper.DeserializeLicenseKeysFromJson(keysAsJson);
		}
		#endregion

		#region Private
		/// <summary>
		/// Возвращает собранный путь до конфигурации ключей.
		/// </summary>
		/// <returns>Путь до файла с конфигурацией ключей.</returns>
		private string BuildConfigPath()
		{
			var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

			var configDirectory = Path.Combine(appdataPath, typeof(LicenseKeysStorage).Assembly.GetName().Name);
			if (!Directory.Exists(configDirectory))
			{
				Directory.CreateDirectory(configDirectory);
			}

			return Path.Combine(configDirectory, ConfigFileName);
		}
		#endregion
	}
}
