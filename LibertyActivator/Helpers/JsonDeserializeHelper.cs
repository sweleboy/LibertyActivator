using LibertyActivator.Exceptions;
using LibertyActivator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LibertyActivator.Helpers
{
	/// <summary>
	/// Представляет помощника для работы с десериализацией JSON.
	/// </summary>
	public class JsonDeserializeHelper
	{
		/// <summary>
		/// Десереализует json контент в коллекцию лицензионных ключей.
		/// </summary>
		/// <param name="jsonContent">Содержимое JSON.</param>
		/// <returns>Коллекция лицензионных ключей.</returns>
		public static IReadOnlyCollection<LicenseKey> DeserializeLicenseKeysFromJson(string jsonContent)
		{
			if (string.IsNullOrEmpty(jsonContent))
			{
				return Array.Empty<LicenseKey>();
			}

			try
			{
				return JsonConvert.DeserializeObject<IReadOnlyCollection<LicenseKey>>(jsonContent);
			}
			catch (JsonException jsonEx)
			{
				throw new ExceptionWithFriendlyMessage("Ошибка при десериализации лицензионных ключей. Причина: исходный файл не содержит лицензионных ключей.", jsonEx);
			}
		}
	}
}
