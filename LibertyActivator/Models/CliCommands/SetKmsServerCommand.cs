namespace LibertyActivator.Models.CliCommands
{
	public class SetKmsServerCommand : ICliCommand
	{
		public string Command => @"slmgr //b /skms kms.digiboy.ir";
	}
}
