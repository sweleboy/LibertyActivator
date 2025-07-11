using LibertyActivator.Models.CliCommands;
using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	public interface ICmdExecutor
	{
		Task ExecuteCommandAsync(params ICliCommand[] commands);
		Task ExecuteCommandWithAdministratorPermissionsAsync(params ICliCommand[] commands);
	}
}
