using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
	public class CmdExecutor : ICmdExecutor
	{
		public async Task ExecuteCommandAcync(params string[] commands)
		{
			var process = BuildProcessStartInfo(commands);
			await ExecuteProcessAsync(process);
		}

		public async Task ExecuteCommandWithAdministratorPermissionsAsync(params string[] commands)
		{
			var process = BuildProcessStartInfo(commands);
			process.Verb = "runas";
			process.UseShellExecute = false;

			await ExecuteProcessAsync(process);
		}

		private async Task ExecuteProcessAsync(ProcessStartInfo processStartInfo)
		{
			try
			{
				using (Process process = Process.Start(processStartInfo))
				{
					process.EnableRaisingEvents = true;

					process.Exited += async (s, e) =>
					{
						if (process.ExitCode != 0)
						{
							MessageHelper.ShowError("Ошибка при выполнении команды", $"Ошибка выполнения команд. Код завершения: {process.ExitCode}\nПопробуйте запустить программу от имени Администратора.");
						}
					};

					await Task.Run(() => process.WaitForExit());
				}
			}
			catch (Exception ex)
			{
				MessageHelper.ShowError("Ошибка при выполнении команды", ex.Message);
			}
		}

		private ProcessStartInfo BuildProcessStartInfo(string[] commands)
		{
			string command = string.Join(" && ", commands);
			return new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = $"/c {command}",
				WindowStyle = ProcessWindowStyle.Hidden,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				CreateNoWindow = true,
			};
		}
	}
}
