using System.Diagnostics;

namespace LibertyActivator.Contracts
{
	/// <summary>
	/// Представляет инструмент для построения процессов.
	/// </summary>
	public interface IProcessBuilder
	{
		/// <summary>
		/// Возвращает построенный процесс командной строки.
		/// </summary>
		/// <param name="command">Команды.</param>
		/// <returns>Процесс.</returns>
		ProcessStartInfo BuildCmdProcessStartInfo(string command);
	}
}
