using System.Diagnostics;
using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	public interface IProcessExecutor
	{
		Task<int> ExecuteAsync(ProcessStartInfo startInfo);
	}
}
