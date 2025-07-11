using LibertyActivator.Models.CliCommands;

namespace LibertyActivator.Contracts
{
	public interface ICmdExecutor
	{
		void ExecuteCommand(params ICliCommand[] commands);
		void ExecuteCommandWithAdministratorPermissions(params ICliCommand[] commands);
	}
}
