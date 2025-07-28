using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
    public interface IFileReader
    {
        Task<string> ReadAsync(string path);
        string Read(string path);
    }
}
