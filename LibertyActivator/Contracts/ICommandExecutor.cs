using LibertyActivator.Models.CliCommands;
using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	/// <summary>
	/// Представляет инструмент выполнения команд в командной строке.
	/// </summary>
	public interface ICommandExecutor
	{
		/// <summary>
		/// Выполняет команду.
		/// </summary>
		/// <param name="commands">Команды.</param>
		/// <returns>Код завершения процесса выполнения</returns>
		Task<int> ExecuteCommandsAsync(params ICliCommand[] commands);
		/// <summary>
		/// Выполняет команды с правами администратора.
		/// </summary>
		/// <param name="commands">Команды.</param>
		/// <returns>Код завершения процесса выполнения</returns>
		Task<int> ExecuteCommandsWithAdministratorPermissionsAsync(params ICliCommand[] commands);
	}
}
