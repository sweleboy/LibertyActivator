using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using LibertyActivator.Models.CliCommands;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
	public class CmdExecutor : ICmdExecutor
	{
		public async Task ExecuteCommandAsync(params ICliCommand[] commands)
		{
			var processStartInfo = BuildCmdProcessStartInfo(commands);
			await ExecuteProcessAsync(processStartInfo);
		}

		public async Task ExecuteCommandWithAdministratorPermissionsAsync(params ICliCommand[] commands)
		{
			var processStartInfo = BuildCmdProcessStartInfo(commands);
			processStartInfo.Verb = "runas";
			processStartInfo.UseShellExecute = false;

			await ExecuteProcessAsync(processStartInfo);
		}

		private async Task ExecuteProcessAsync(ProcessStartInfo processStartInfo)
		{
			try
			{
				using (Process process = new Process { StartInfo = processStartInfo })
				{
					var tcs = new TaskCompletionSource<bool>();

					process.EnableRaisingEvents = true;
					process.Exited += (s, e) =>
					{
						if (process.ExitCode != 0)
						{
							MessageHelper.ShowError("Ошибка при выполнении команды",
								$"Ошибка выполнения команд. Код завершения: {process.ExitCode}\n" +
								"Попробуйте запустить программу от имени Администратора.");
						}
						tcs.TrySetResult(true);
					};

					process.Start();
					await tcs.Task;
				}
			}
			catch (Exception ex)
			{
				MessageHelper.ShowError("Ошибка при выполнении команды", ex.Message);
			}
		}

		private ProcessStartInfo BuildCmdProcessStartInfo(ICliCommand[] commands)
		{
			string command = string.Join(" && ", commands.Select(x => x.Command));
			return new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = $"/C {command}",
				WindowStyle = ProcessWindowStyle.Hidden,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				CreateNoWindow = true,
			};
		}
	}
}
