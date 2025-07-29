using LibertyActivator.Exceptions;
using LibertyActivator.Helpers;
using System;
using System.Windows.Input;

namespace LibertyActivator.Commands
{
	/// <summary>
	/// Представляет безопасные асинхронные команды для переключения кнопок.
	/// </summary>
	public class SafeRelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Func<object, bool> _canExecute;

		public event EventHandler CanExecuteChanged;

		public SafeRelayCommand(Action execute, Func<bool> canExecute = null)
			: this(_ => execute(), canExecute != null ? new Func<object, bool>(param => canExecute()) : null) { }

		public SafeRelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

		public void Execute(object parameter)
		{
			try
			{
				_execute(parameter);
			}
			catch(ExceptionWithFriendlyMessage exc)
			{
				MessageHelper.ShowError("Ошибка", exc.Message);
			}
		}

		public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
