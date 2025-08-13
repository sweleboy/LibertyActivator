using LibertyActivator.Contracts;
using LibertyActivator.Exceptions;
using LibertyActivator.Helpers;
using LibertyActivator.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет инструмент взаимодействия с лицензионными ключами.
	/// </summary>
	public class LicenseKeyService : ILicenseKeyService
	{
		private readonly ILicenseKeysStorage _licenseKeysStorage;
		private readonly RemoteFileReader _remoteFileReader;

		public LicenseKeyService(ILicenseKeysStorage licenseKeysStorage, RemoteFileReader remoteFileReader)
		{
			_licenseKeysStorage = licenseKeysStorage;
			_remoteFileReader = remoteFileReader;
		}

		/// <inheritdoc/>
		public LicenseKey GetKeyByName(string name)
		{
			var keys = _licenseKeysStorage.GetKeys();
			return keys.FirstOrDefault(x => x.Name.Equals(name));
		}

		/// <inheritdoc/>
		public async Task<IReadOnlyCollection<LicenseKey>> DownloadKeysFromRemoteSourceAsync()
		{
			string licenceKeysRemoteUrl = ConfigurationManager.AppSettings["LicenceKeysRemoteUrl"]
				?? throw new ExceptionWithFriendlyMessage("Ссылка на получение ключей не указана в конфигурации.");

			var keysAsJson = await _remoteFileReader.ReadAsync(licenceKeysRemoteUrl);
			return JsonDeserializeHelper.DeserializeLicenseKeysFromJson(keysAsJson);
		}

		/// <inheritdoc/>
		public async Task SaveKeysToFileAsync(string filePath, IReadOnlyCollection<LicenseKey> keys)
		{
			var serializedKeys = JsonConvert.SerializeObject(keys);
			await File.WriteAllTextAsync(filePath, serializedKeys);
		}
	}
}
