namespace LibertyActivator.Models
{
	public class LicenseKey
	{
		public string Name
		{
			get;
			set;
		}

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
