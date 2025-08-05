namespace LibertyActivator.Models.CliCommands
{
	/// <summary>
	/// Представляет команду для установки сервера активации KMS.
	/// </summary>
	public class SetKmsServerCommand : ICliCommand
	{
		/// <inheritdoc/>
		public string Command => @"slmgr //b /skms kms.digiboy.ir";
	}
}
