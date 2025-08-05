namespace LibertyActivator.Models.CliCommands
{
	/// <summary>
	/// Представляет комманду выполнения для активации системы Windows.
	/// </summary>
	public class ActivateWindowsCommand : ICliCommand
	{
		/// <inheritdoc/>
		public string Command => @"slmgr //b /ato";
	}
}
