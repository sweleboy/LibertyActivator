using LibertyActivator.Contracts;
using LibertyActivator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LibertyActivator.Services
{
	public class LicenseKeysStorage : ILicenseKeysStorage
	{
		private const string ConfigFileName = "keys.json";
		private readonly string _configPath;
		private readonly LocalFileReader _localFileReader;
		private readonly RemoteFileReader _remoteFileReader;

		public LicenseKeysStorage(LocalFileReader localFileReader, RemoteFileReader remoteFileReader)
		{
			_configPath = BuildConfigPath();
			_localFileReader = localFileReader;
			_remoteFileReader = remoteFileReader;
		}
		public string GetConfigPath() => _configPath;
		public IReadOnlyCollection<LicenseKey> GetKeys()
		{
			var keysAsJson = _localFileReader.Read(_configPath);
			return JsonConvert.DeserializeObject<IReadOnlyCollection<LicenseKey>>(keysAsJson);
		}
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
	}
}
