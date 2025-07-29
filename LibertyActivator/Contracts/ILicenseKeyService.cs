using LibertyActivator.Models;
using System.Collections.Generic;

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
	}
}
