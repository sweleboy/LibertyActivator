using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using LibertyActivator.Models.CliCommands;
using System;
using System.Diagnostics;
using System.Linq;

namespace LibertyActivator.Services
{
	public class CmdExecutor : ICmdExecutor
	{
		public void ExecuteCommand(params ICliCommand[] commands)
		{
			var processStartInfo = BuildCmdProcessStartInfo(commands);
			ExecuteProcess(processStartInfo);
		}

		public void ExecuteCommandWithAdministratorPermissions(params ICliCommand[] commands)
		{
			var processStartInfo = BuildCmdProcessStartInfo(commands);
			processStartInfo.Verb = "runas";
			processStartInfo.UseShellExecute = false;

			ExecuteProcess(processStartInfo);
		}

		private void ExecuteProcess(ProcessStartInfo processStartInfo)
		{
			try
			{
				using (Process process = Process.Start(processStartInfo))
				{
					process.EnableRaisingEvents = true;

					process.Exited += (s, e) =>
					{
						if (process?.ExitCode != 0)
						{
							MessageHelper.ShowError("Ошибка при выполнении команды", $"Ошибка выполнения команд. Код завершения: {process.ExitCode}\nПопробуйте запустить программу от имени Администратора.");
						}
					};
					process.WaitForExit();
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
