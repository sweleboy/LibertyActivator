using LibertyActivator.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
	/// <summary>
	/// Представляет локальный инструмент для чтения файлов.
	/// </summary>
	public class RemoteFileReader : IFileReader
	{
		/// <inheritdoc/>
		public string Read(string url)
		{
			using (var httpClient = new HttpClient())
			{
				var content = httpClient.GetStringAsync(url)
										.GetAwaiter()
										.GetResult();
				return content;
			}
		}

		/// <inheritdoc/>
		public async Task<string> ReadAsync(string url)
		{
			using (var httpClient = new HttpClient())
			{
				var content = await httpClient.GetStringAsync(url);
				return content;
			}
		}
	}
}
