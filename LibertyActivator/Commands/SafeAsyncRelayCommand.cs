using LibertyActivator.Contracts;
using LibertyActivator.Exceptions;
using LibertyActivator.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Prism.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibertyActivator.Commands
{
	/// <summary>
	/// Представляет безопасные асинхронные команды для переключения кнопок.
	/// </summary>
	public class SafeAsyncRelayCommand : ICommand
	{
		#region Fields
		private readonly Func<object, Task> _execute;
		private readonly Func<object, bool> _canExecute;
		private bool _isExecuting;
		#endregion

		#region Public
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
				var exceptionHandler = ((App)Application.Current).ServiceProvider.GetRequiredService<IExceptionHandler>() 
					?? throw new ArgumentNullException(nameof(IExceptionHandler));
				exceptionHandler.Handle(exc);
			}
			finally
			{
				_isExecuting = false;
				RaiseCanExecuteChanged();
			}
		}

		public void RaiseCanExecuteChanged() =>
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		#endregion
	}
}
