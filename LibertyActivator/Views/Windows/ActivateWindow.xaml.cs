using System;
using System.Windows;

namespace LibertyActivator.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для ActivateWindow.xaml
	/// </summary>
	public partial class ActivateWindow : Window
	{
		public ActivateWindow()
		{
			InitializeComponent();
		}

		private void HeaderPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private void MinimizeButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
			Environment.Exit(0);
		}
	}
}
