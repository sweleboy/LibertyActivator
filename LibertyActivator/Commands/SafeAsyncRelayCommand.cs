using LibertyActivator.Exceptions;
using LibertyActivator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibertyActivator.Commands
{
	public class SafeAsyncRelayCommand : ICommand
	{
		private readonly Func<object, Task> _execute;
		private readonly Func<object, bool> _canExecute;
		private bool _isExecuting;

		public event EventHandler CanExecuteChanged;

		public SafeAsyncRelayCommand(
			Func<Task> execute,
			Func<bool> canExecute = null)
			: this(_ => execute(), canExecute != null ? new Func<object, bool>(param => canExecute()) : null) { }

		public SafeAsyncRelayCommand(
			Func<object, Task> execute,
			Func<object, bool> canExecute = null)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			if (_isExecuting)
				return false;

			return _canExecute?.Invoke(parameter) ?? true;
		}

		public async void Execute(object parameter)
		{
			if (!CanExecute(parameter))
				return;

			try
			{
				_isExecuting = true;
				RaiseCanExecuteChanged();

				await _execute(parameter);
			}
			catch (ExceptionWithFriendlyMessage exc)
			{
				MessageHelper.ShowError("Ошибка", exc.Message);
			}
			finally
			{
				_isExecuting = false;
				RaiseCanExecuteChanged();
			}
		}

		public void RaiseCanExecuteChanged() =>
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
}
