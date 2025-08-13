using LibertyActivator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibertyActivator.Contracts
{
	/// <summary>
	/// Представляет инструмент взаимодействия с лицензионными ключами.
	/// </summary>
	public interface ILicenseKeyService
	{
		/// <summary>
		/// Возвращает ключ по наименованию.
		/// </summary>
		/// <param name="name">Наименование ключа.</param>
		/// <returns>Лицензионный ключ.</returns>
		LicenseKey GetKeyByName(string name);
		/// <summary>
		/// Скачивает ключи из удалённого источника.
		/// </summary>
		/// <returns>Коллекция ключей.</returns>
		Task<IReadOnlyCollection<LicenseKey>> DownloadKeysFromRemoteSourceAsync();
		/// <summary>
		/// Сохраняет ключи в файл.
		/// </summary>
		/// <returns>Путь до файла.</returns>
		Task SaveKeysToFileAsync(string filePath, IReadOnlyCollection<LicenseKey> keys);
	}
}
