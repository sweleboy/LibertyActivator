using LibertyActivator.Exceptions;
using System.Text.RegularExpressions;

namespace LibertyActivator.Models
{
	/// <summary>
	/// Представляет лицензионный ключ.
	/// </summary>
	public class LicenseKey
	{
		/// <summary>
		/// Наименование ключа.
		/// </summary>
		public string Name
		{
			get;
			init;
		}
		/// <summary>
		/// Ключ.
		/// </summary>
		public string Key
		{
			get;
			private init;
		}
		public LicenseKey(string name, string key)
		{
			CheckKey(key);

			Name = name;
			Key = key;
		}

		/// <summary>
		/// Выполняет проверку ключа.
		/// </summary>
		/// <param name="key">Ключ.</param>
		/// <exception cref="ExceptionWithFriendlyMessage">В случае если ключ не подходит.</exception>
		private void CheckKey(string key)
		{
			if (!IsValidKey(key))
			{
				throw new ExceptionWithFriendlyMessage("Не удалось создать ключ. Причина: ключ содержит не допустимое значение. Пример допустимого значения: \"XXXXX-XXXXX-XXXXX-XXXXX-XXXXX\"");
			}
		}

		/// <summary>
		/// Проверяет валидность ключа.
		/// </summary>
		/// <param name="key">Ключ.</param>
		/// <returns>Результат проверки, истина-если ключ валиден.</returns>
		private bool IsValidKey(string key)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return false;
			}

			var pattern = @"^[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}$";
			return Regex.IsMatch(key, pattern, RegexOptions.IgnoreCase);
		}
	}
}
