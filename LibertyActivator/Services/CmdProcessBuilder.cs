using LibertyActivator.Contracts;
using System.Diagnostics;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет инструмент для построения процессов связанных с командной строкой.
	/// </summary>
	public class CmdProcessBuilder : IProcessBuilder
	{
		/// <inheritdoc/>
		public ProcessStartInfo BuildCmdProcessStartInfo(string command)
		{
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
