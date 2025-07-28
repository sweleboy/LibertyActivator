using System.IO;

namespace LibertyActivator.Helpers
{
    public class FileHelper
    {
        public static void ThrowIfFileNotExist(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Не удалось найти файл. Причина: файл не существует.");
            }
        }
    }
}
