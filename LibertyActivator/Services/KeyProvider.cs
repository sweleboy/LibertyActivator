using LibertyActivator.Models;

namespace LibertyActivator.Services
{
	public static class KeyProvider
	{
		private static LicenseKey _licenseKey;
		public static LicenseKey GetLicenseKey() => _licenseKey;
		public static void SetLicenseKey(LicenseKey licenseKey)
		{
			_licenseKey = licenseKey;
		}
	}
}
