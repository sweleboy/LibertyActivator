using LibertyActivator.Constants;
using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using LibertyActivator.Models.CliCommands;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
	public class CommandExecutor : ICommandExecutor
	{
		private readonly IProcessBuilder _processBuilder;
		private readonly IProcessExecutor _processExecutor;

		public CommandExecutor(
			IProcessBuilder processBuilder,
			IProcessExecutor processExecutor)
		{
			_processBuilder = processBuilder;
			_processExecutor = processExecutor;
		}

		public async Task<int> ExecuteCommandsAsync(params ICliCommand[] commands)
		{
			return await ExecuteCommandsAsync(commands, runAsAdmin: true);
		}

		public async Task<int> ExecuteCommandsWithAdministratorPermissionsAsync(params ICliCommand[] commands)
		{
			return await ExecuteCommandsAsync(commands, runAsAdmin: true);
		}

		private async Task<int> ExecuteCommandsAsync(ICliCommand[] commands, bool runAsAdmin)
		{
			try
			{
				var command = string.Join(" && ", commands.Select(x => x.Command));
				var startInfo = _processBuilder.BuildCmdProcessStartInfo(command);

				if (runAsAdmin == true)
				{
					startInfo.Verb = "runas";
					startInfo.UseShellExecute = false;
				}

				var exitCode = await _processExecutor.ExecuteAsync(startInfo);
				return exitCode;
			}
			catch
			{
				MessageHelper.ShowError("Ошибка", "Произошла непредвиденая ошибка при выполнении команды");
				return ExitCodes.FailureExitCode;
			}
		}
	}
}
