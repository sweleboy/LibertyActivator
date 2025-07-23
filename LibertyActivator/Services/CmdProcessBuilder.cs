using LibertyActivator.Contracts;
using System.Diagnostics;

namespace LibertyActivator.Services
{
	public class CmdProcessBuilder : IProcessBuilder
	{
		public ProcessStartInfo BuildProcessStartInfo(string command)
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
