using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	/// <summary>
	/// Представляет инструмент для чтения файлов.
	/// </summary>
	public interface IFileReader
	{
		/// <summary>
		/// Читает содержимое файла асинхронно.
		/// </summary>
		/// <param name="path">Путь до файла.</param>
		/// <returns>Содежимое прочтённого файла.</returns>
		Task<string> ReadAsync(string path);
		/// <summary>
		/// Читает содержимое файла синхронно.
		/// </summary>
		/// <param name="path">Путь до файла.</param>
		/// <returns>Содежимое прочтённого файла.</returns>
		string Read(string path);
	}
}
