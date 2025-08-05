using LibertyActivator.Contracts;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет инструмент выполнения процессов.
	/// </summary>
	public class ProcessExecutor : IProcessExecutor
	{
		/// <inheritdoc/>
		public async Task<int> ExecuteAsync(ProcessStartInfo startInfo)
		{
			using (var process = new Process { StartInfo = startInfo })
			{
				var tcs = new TaskCompletionSource<int>();
				process.EnableRaisingEvents = true;
				process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);
				process.Start();
				return await tcs.Task;
			}
		}
	}
}
