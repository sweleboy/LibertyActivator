namespace LibertyActivator.Models.CliCommands
{
	/// <summary>
	/// Представялет комманду для выполненния в терминале.
	/// </summary>
	public interface ICliCommand
	{
		/// <summary>
		/// Текст команды.
		/// </summary>
		string Command
		{
			get;
		}

	}
}
