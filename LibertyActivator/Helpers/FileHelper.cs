using System.IO;

namespace LibertyActivator.Helpers
{
    /// <summary>
    /// Представляет помощника для работы с файлами.
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Вызывает исключение, если файла не сущесвует.
        /// </summary>
        /// <param name="path">Путь до файла.</param>
        public static void ThrowIfFileNotExist(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Не удалось найти файл. Причина: файл не существует.");
            }
        }
    }
}
