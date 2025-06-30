using LibertyActivator.Models;
using System.Collections.Generic;

namespace LibertyActivator.Contracts
{
	public interface ILicenseKeyService
	{
		LicenseKey GetKeyByName(string name);
	}
}
