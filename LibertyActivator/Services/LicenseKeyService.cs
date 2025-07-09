using LibertyActivator.Contracts;
using LibertyActivator.Models;
using System.Linq;

namespace LibertyActivator.Services
{
	public class LicenseKeyService : ILicenseKeyService
	{
		private readonly ILicenseKeysStorage _licenseKeysStorage;

		public LicenseKeyService(ILicenseKeysStorage licenseKeysStorage)
		{
			_licenseKeysStorage = licenseKeysStorage;
		}
		public LicenseKey GetKeyByName(string name)
		{
			var keys = _licenseKeysStorage.GetKeys();
			return keys.FirstOrDefault(x => x.Name.Equals(name));
		}
	}
}
