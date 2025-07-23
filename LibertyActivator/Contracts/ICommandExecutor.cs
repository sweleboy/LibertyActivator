using LibertyActivator.Models.CliCommands;
using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	public interface ICommandExecutor
	{
		Task<int> ExecuteCommandAsync(params ICliCommand[] commands);
		Task<int> ExecuteCommandWithAdministratorPermissionsAsync(params ICliCommand[] commands);
	}
}
