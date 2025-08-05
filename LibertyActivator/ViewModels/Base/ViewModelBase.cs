using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LibertyActivator.ViewModels.Base
{
	/// <summary>
	/// Представляет базовый класс модели представления.
	/// </summary>
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected ViewModelBase()
		{
			InitializeCommands();
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		/// <summary>
		/// Устанавливает значение в поле/свойство с вызовом уведомления об изменении.
		/// </summary>
		/// <param name="backingField">Поле/свойство.</param>
		/// <param name="value">Значение.</param>
		/// <param name="propertyName">Наименование поля/свойства.</param>
		protected virtual bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingField, value))
				return false;

			backingField = value;
			OnPropertyChanged(propertyName);
			return true;
		}
		/// <summary>
		/// Выполняет инициализацию команд.
		/// </summary>
		protected virtual void InitializeCommands()
		{
			return;
		}
	}
}
