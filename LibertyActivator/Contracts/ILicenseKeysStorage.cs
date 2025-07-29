using LibertyActivator.Models;
using System.Collections.Generic;

namespace LibertyActivator.Contracts
{
	/// <summary>
	/// Представляет инструмент для взаимодействия с хранилищем лицензионных ключей.
	/// </summary>
	public interface ILicenseKeysStorage
	{
		/// <summary>
		/// Возвращает все ключи.
		/// </summary>
		/// <returns>Коллеция лицензионных ключей.</returns>
		IReadOnlyCollection<LicenseKey> GetKeys();
		/// <summary>
		/// Получает путь до файла с конфигурацией ключей.
		/// </summary>
		/// <returns>Путь до файла.</returns>
		string GetConfigPath();
	}
}
