using LibertyActivator.Exceptions;
using LibertyActivator.Models.CliCommands;

namespace LibertyActivator.Models.Commands
{
	public class SetProductKeyCliCommand : ICliCommand
	{
		private readonly string _key = null;
		public string Command => $@"slmgr //b /ipk {GetKey()}";
		private SetProductKeyCliCommand(string key)
		{
			_key = key;
		}
		public static SetProductKeyCliCommand Create(LicenseKey licenseKey)
		{
			if (string.IsNullOrEmpty(licenseKey.Key))
			{
				throw new ExceptionWithFriendlyMessage("Не возможно инициализировать команду установки ключа. Причина: ключ не может быть пустым");
			}

			return new SetProductKeyCliCommand(licenseKey.Key);
		}

		private string GetKey()
		{
			return _key ?? throw new ExceptionWithFriendlyMessage("Не возможно обратиться к ключу команды установки ключа. Причина: ключ не может быть пустым");
		}
	}
}
