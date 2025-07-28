using LibertyActivator.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
	public class RemoteFileReader : IFileReader
	{
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
