using LibertyActivator.Models;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет провайдер лицензионных ключей.
	/// </summary>
	public static class KeyProvider
	{
		private static LicenseKey _licenseKey;

		/// <summary>
		/// Возвразает загруженные ключи.
		/// </summary>
		/// <returns>Коллекция лицензионных ключей.</returns>
		public static LicenseKey GetLicenseKey() => _licenseKey;

		/// <summary>
		/// Устанавливает/загружает лицензионные ключи.
		/// </summary>
		/// <param name="licenseKey"></param>
		public static void SetLicenseKey(LicenseKey licenseKey)
		{
			_licenseKey = licenseKey;
		}
	}
}
