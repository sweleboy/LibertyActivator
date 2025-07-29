using System.Diagnostics;
using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	/// <summary>
	/// Представляет инструмент выполнения процессов.
	/// </summary>
	public interface IProcessExecutor
	{
		/// <summary>
		/// Выполняет процесс.
		/// </summary>
		/// <param name="startInfo">Информация о процессе.</param>
		/// <returns>Код выполнения(завершения) процесса.</returns>
		Task<int> ExecuteAsync(ProcessStartInfo startInfo);
	}
}
