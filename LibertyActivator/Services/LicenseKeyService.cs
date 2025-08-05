using LibertyActivator.Contracts;
using LibertyActivator.Models;
using System.Linq;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет инструмент взаимодействия с лицензионными ключами.
	/// </summary>
	public class LicenseKeyService : ILicenseKeyService
	{
		private readonly ILicenseKeysStorage _licenseKeysStorage;

		public LicenseKeyService(ILicenseKeysStorage licenseKeysStorage)
		{
			_licenseKeysStorage = licenseKeysStorage;
		}

		/// <inheritdoc/>
		public LicenseKey GetKeyByName(string name)
		{
			var keys = _licenseKeysStorage.GetKeys();
			return keys.FirstOrDefault(x => x.Name.Equals(name));
		}
	}
}
