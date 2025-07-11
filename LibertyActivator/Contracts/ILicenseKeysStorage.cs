using LibertyActivator.Models;
using System.Collections.Generic;

namespace LibertyActivator.Contracts
{
	public interface ILicenseKeysStorage
	{
		IReadOnlyCollection<LicenseKey> GetKeys();
		string GetConfigPath();
	}
}
