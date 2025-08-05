using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет локальный инструмент для чтения файлов.
	/// </summary>
	public class LocalFileReader : IFileReader
    {
		/// <inheritdoc/>
		public string Read(string path)
        {
            FileHelper.ThrowIfFileNotExist(path);

            return File.ReadAllText(path);
		}

		/// <inheritdoc/>
		public async Task<string> ReadAsync(string path)
        {
            FileHelper.ThrowIfFileNotExist(path);

            using (var reader = new StreamReader(path))
            {
                var content = await reader.ReadToEndAsync();
                return content;
            }
        }
    }
}
