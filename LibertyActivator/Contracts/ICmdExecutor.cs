using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	public interface ICmdExecutor
	{
		Task ExecuteCommandAcync(params string[] commands);
		Task ExecuteCommandWithAdministratorPermissionsAsync(params string[] commands);
	}
}
