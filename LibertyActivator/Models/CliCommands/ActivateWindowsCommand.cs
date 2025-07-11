namespace LibertyActivator.Models.CliCommands
{
	public class ActivateWindowsCommand : ICliCommand
	{
		public string Command => @"slmgr //b /ato";
	}
}
