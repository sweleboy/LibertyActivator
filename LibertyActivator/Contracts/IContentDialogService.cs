using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibertyActivator.Contracts
{
	/// <summary>
	/// Представляет инструмент взаимодействия с диалогом.
	/// </summary>
	public interface IContentDialogService
	{
		/// <summary>
		/// Показывает диалог.
		/// </summary>
		/// <param name="title">Заголовок.</param>
		/// <param name="contentControl">Сожержимое.</param>
		Task ShowDialogAsync(string title, UserControl contentControl);
		/// <summary>
		/// Закрывает диалог.
		/// </summary>
		void CloseDialog();
	}
}
