using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Socrates
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		#region Properties

		public static bool AutoStartOnLogon
		{
			get { return Socrates.Properties.Settings.Default.AutoStartOnLogon; }
			set { Socrates.Properties.Settings.Default.AutoStartOnLogon = value; }
		}

		#endregion

		#region Methods

		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Socrates.MainWindow.Instance.SearchText = null;
			MessageBox.Show(e.Exception.Message, MainWindow.Title, MessageBoxButton.OK, MessageBoxImage.Error);
			e.Handled = true;
		}

		protected override void OnExit(ExitEventArgs e)
		{
			Socrates.Properties.Settings.Default.Save();
			base.OnExit(e);
		}

		#endregion

		#region Constructor

		public App()
		{
			Socrates.Properties.Settings.Default.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
			{
				if (e.PropertyName.CompareTo(nameof(Socrates.Properties.Settings.Default.AutoStartOnLogon)) == 0)
					using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
						if (Socrates.Properties.Settings.Default.AutoStartOnLogon)
							key.SetValue(System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase), Assembly.GetExecutingAssembly().Location, RegistryValueKind.String);
						else
							key.DeleteValue(Assembly.GetCallingAssembly().FullName);
			};
		}

		#endregion
	}
}
