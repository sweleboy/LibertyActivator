using System.Diagnostics;

namespace LibertyActivator.Contracts
{
	public interface IProcessBuilder
	{
		ProcessStartInfo BuildProcessStartInfo(string command);
	}
}
