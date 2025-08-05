using LibertyActivator.Exceptions;
using LibertyActivator.Models.CliCommands;

namespace LibertyActivator.Models.Commands
{
	/// <summary>
	/// Представляет комманду для установки ключа активации продукта.
	/// </summary>
	public class SetProductKeyCliCommand : ICliCommand
	{
		#region Data
		private readonly string _key = null;

		/// <inheritdoc/>
		public string Command => $@"slmgr //b /ipk {GetKey()}";
		#endregion

		#region .ctor
		private SetProductKeyCliCommand(string key)
		{
			_key = key;
		}
		#endregion

		#region Public
		/// <summary>
		/// Создаёт команду <see cref="SetProductKeyCliCommand"/>
		/// </summary>
		/// <param name="licenseKey">Лицензионный ключ.</param>
		public static SetProductKeyCliCommand Create(LicenseKey licenseKey)
		{
			if (string.IsNullOrEmpty(licenseKey.Key))
			{
				throw new ExceptionWithFriendlyMessage("Не возможно инициализировать команду установки ключа. Причина: ключ не может быть пустым");
			}

			return new SetProductKeyCliCommand(licenseKey.Key);
		}
		#endregion

		#region Private
		/// <summary>
		/// Возвращает лицензионный ключ.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="ExceptionWithFriendlyMessage"></exception>
		private string GetKey()
		{
			return _key ?? throw new ExceptionWithFriendlyMessage("Не возможно обратиться к ключу команды установки ключа. Причина: ключ не может быть пустым");
		}
		#endregion
	}
}
