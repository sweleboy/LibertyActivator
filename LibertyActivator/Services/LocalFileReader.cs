using LibertyActivator.Contracts;
using LibertyActivator.Helpers;
using System.IO;
using System.Threading.Tasks;

namespace LibertyActivator.Services
{
    public class LocalFileReader : IFileReader
    {
        public string Read(string path)
        {
            FileHelper.ThrowIfFileNotExist(path);

            return File.ReadAllText(path);
        }

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
