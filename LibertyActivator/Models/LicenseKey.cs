namespace LibertyActivator.Models
{
	/// <summary>
	/// Представляет лицензионный ключ.
	/// </summary>
	public class LicenseKey
	{
		/// <summary>
		/// Наименование ключа.
		/// </summary>
		public string Name
		{
			get;
			set;
		}
		/// <summary>
		/// Ключ.
		/// </summary>
		public string Key
		{
			get;
			set;
		}
		public LicenseKey(string name, string key)
		{
			Name = name;
			Key = key;
		}
	}
}
