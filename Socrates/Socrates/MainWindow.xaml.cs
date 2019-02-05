using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Socrates
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Fields

		public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register("SearchText", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

		#endregion

		#region Properties

		public static MainWindow Instance { get; private set; }

		public KeyboardHook KeyboardHook { get; }

		public string SearchText
		{
			get { return (string)GetValue(SearchTextProperty); }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					TextBoxSearch.Foreground = Brushes.AliceBlue;
					BorderHelp.Visibility = Visibility.Collapsed;
				}
				SetValue(SearchTextProperty, value);
			}
		}

        public List<string> CommandHistory { get; }

		#endregion

		#region Methods

		protected void CommandRecognition()
		{
			BorderHelp.Visibility = Visibility.Collapsed;
			string command = SearchText;
			if (!string.IsNullOrEmpty(command))
				if (command.StartsWith(":"))
				{
					BorderHelp.Visibility = Visibility.Visible;
					int index = command.IndexOf(' ');
					command = index >= 0 ? command.Substring(1, index - 1) : command.Substring(1);
					if (!string.IsNullOrEmpty(command))
					{
						if (command.ToCommand<InternalCommands>() != InternalCommands.Default)
							TextBoxSearch.Foreground = Brushes.LimeGreen;
						else
							TextBoxSearch.Foreground = Brushes.AliceBlue;
					}
					UpdateHelpHints(command);
				}
		}

		protected void ProcessCommand(string command, string args = null)
		{
			if (!string.IsNullOrEmpty(command))
			{
                bool commandIsOk = true;
				switch (command.ToCommand<InternalCommands>())
				{
					case InternalCommands.AnonymZ:
						if (!string.IsNullOrEmpty(args))
							if (args.StartsWith("https://") || args.StartsWith("http://"))
								Process.Start(string.Format("https://anonymz.com/?{0}", args));
							else
								Process.Start(string.Format("https://anonymz.com/?https://{0}", args));
						else
							goto default;
						break;

					case InternalCommands.Csfd:
						if (string.IsNullOrEmpty(args))
							Process.Start("https://www.csfd.com");
						else
							Process.Start(string.Format("https://www.csfd.cz/hledat/?q={0}", args.ParseUrl().Replace(' ', '+')));
						break;

					case InternalCommands.Exit:
						Close();
						break;

					case InternalCommands.GoogleTranslate:
						{
							int index = args.IndexOf(' ');
							string[] langs = args.Substring(0, index).ParseGoogleLanguages();
							string search = args.Substring(index + 1, args.Length - index - 1);
							if (!string.IsNullOrEmpty(args))
								// #view=home&op=translate&sl=auto&tl=ky&text=hello
								//Process.Start(string.Format("https://translate.google.com/#{0}/{1}/{2}", langs[0], langs[1], search.ParseUrl().Replace(' ', '+')));
								Process.Start(string.Format("https://translate.google.com/#view=home&op=translate&sl={0}&tl={1}&text={2}", langs[0], langs[1], search.ParseUrl().Replace(' ', '+')));
							else
								Process.Start("https://translate.google.com");
						}
						break;

					case InternalCommands.Imdb:
						if (!string.IsNullOrEmpty(args))
						{
							string[] argsPieces = args.Split(' ');
							string searchType = null, search = null;
							if (argsPieces.Length > 1)
							{
								if (InternalCommands.Imdb.ContainsParameter(argsPieces[0]))
								{
									searchType = argsPieces[0];
									search = string.Join("+", argsPieces.ToList().GetRange(1, argsPieces.Length - 1).ToArray()).ParseUrl();
								}
								else
								{
									searchType = "all";
									search = argsPieces[0];
								}
							}
							else
							{
								searchType = "all";
								search = argsPieces[0];
							}
							string url = string.Format("https://www.imdb.com/find?ref_=nv_sr_fn&q={1}&s={0}", searchType, search);
							Process.Start(url);
						}
						else
							Process.Start("https://www.imdb.org");
						break;

					case InternalCommands.Metacritic:
						if (string.IsNullOrEmpty(args))
							Process.Start("https://www.metacritic.com/");
						else
							Process.Start(string.Format("https://www.metacritic.com/search/all/{0}/results", args.ParseUrl().Replace(" ", "%20")));
						break;

					case InternalCommands.RottenTomatoes:
						if (string.IsNullOrEmpty(args))
							Process.Start("https://www.rottentomatoes.com/");
						else
							Process.Start(string.Format("https://www.rottentomatoes.com/search/?search={0}", args.ParseUrl().Replace(" ", "%20")));
						break;

					case InternalCommands.Shutdown:
						{
							var psi = new ProcessStartInfo("shutdown", "/s /t 0");
							psi.CreateNoWindow = true;
							psi.UseShellExecute = false;
							Process.Start(psi);
						}
						break;

					case InternalCommands.TvGuide:
						// Year-Month-Day: http://www.horrorchannel.co.uk/tv_guide.php?date=2018-04-01&section=day
						if (!string.IsNullOrEmpty(args))
							if (args.ToUpper().CompareTo("HORROR") == 0)
							{
								DateTime now = DateTime.Now;
								int year = now.Year;
								int month = now.Month;
								int day = now.Day;
								int hour = now.Hour;
								int minute = now.Minute;
								string section = null;
								if ((hour >= 0) && ((hour < 4) && (minute < 59)))
									day--;
								if ((hour >= 4) && (minute >= 20) && (hour < 13) && (minute  <= 59))
									section = "morning";
								else if ((hour >= 13) && (minute >= 0) && (hour < 18) && (minute <= 59))
									section = "afternoon";
								else
									section = "evening";
								Process.Start(string.Format("http://www.horrorchannel.co.uk/tv_guide.php?date={0}-{1:00}-{2:00}&section={3}", year, month, day, section));
							}
						//	else
						//		Process.Start(string.Format("https://www.tvguide.co.uk/search/?keyword={0}", args));
						//else
							Process.Start("https://www.tvguide.co.uk/");
						break;

					case InternalCommands.Wikipedia:
						{
							if (!string.IsNullOrEmpty(args))
							{
								string[] argsPieces = args.Split(' ');
								string lang = null, search = null;

								if (argsPieces.Length > 1)
								{
									lang = argsPieces[0].ParseGoogleLanguages()?[1];
									if (string.IsNullOrEmpty(lang))
									{
										lang = "www";
										search = string.Join("%20", argsPieces).ParseUrl();
									}
									else
										search = string.Join("%20", argsPieces.ToList().GetRange(1, argsPieces.Length - 1).ToArray()).ParseUrl();
								}
								else
								{
									lang = "www";
									search = argsPieces[0].ParseUrl().Replace(" ", "%20");
								}
								string url = string.Format("https://{0}.wikipedia.org/w/index.php?search={1}", lang, search);
								Process.Start(url);
								//https://{0}.wikipedia.org/wiki/Special:Search/{1}
								//https://cs.wikipedia.org/w/index.php?search=Kozlik+lekarsky
							}
							else
								Process.Start("https://www.wikipedia.org/");
						}
						break;

					case InternalCommands.YouTube:
						if (string.IsNullOrEmpty(args))
							Process.Start("https://www.youtube.com");
						else
							Process.Start(string.Format("https://www.youtube.com/results?search_query={0}", args.ParseUrl().Replace(' ', '+')));
						break;

					default:
						System.Media.SystemSounds.Exclamation.Play();
                        commandIsOk = false;
						break;
				}
                if (commandIsOk)
                    CommandHistory.Add(string.Format("{0} {1}", command, args));
			}
			BorderHelp.Visibility = Visibility.Collapsed;
		}

		protected void ProcessSearch()
		{
			string line = SearchText;
			if (string.IsNullOrEmpty(line))
				return;
			SearchText = null;
			if (line.StartsWith(":"))
			{
				int index = line.IndexOf(' ');
				if (index >= 0)
					ProcessCommand(line.Substring(1, index - 1), line.Substring(index + 1, line.Length - index - 1));
				else
					ProcessCommand(line.Substring(1));
			}
			else if (line.Contains(".") || line.Contains(":") || line.Contains("//"))
				Process.Start(string.Format("https://{0}", line));
			else
				Process.Start(string.Format("https://duckduckgo.com/?q={0}", line));
		}

		public void UpdateHelpHints(string command)
		{
			FlowDocumentHelp.Blocks.Clear();
			string commandHelpHints = null;
			Array commands = Enum.GetValues(typeof(InternalCommands));
			if (InternalCommands.GoogleTranslate.GetAttribute<CommandNameAttribute>().Commands[0].CompareTo(command) != 0)
				for (int index = 0; index < commands.Length; index++)
				{
					InternalCommands iCommand = (InternalCommands)commands.GetValue(index);
					string description = iCommand.GetAttribute<CommandDescriptionAttribute>()?.Description;
					string name = iCommand.GetAttribute<CommandNameAttribute>()?.Commands.FirstOrDefault();
					string usage = iCommand.GetAttribute<CommandUsageAttribute>()?.Usage;
					if (!string.IsNullOrEmpty(description) && !string.IsNullOrEmpty(name))
					{
						string tabs = "\t";
						if (name.Length < 8)
							tabs += "\t";
						string commandHelpHint = null;
						if ((index < commands.Length) && (index < commands.Length - 1))
						{
							commandHelpHint = string.Format("<Bold>{0}</Bold>{1}{2}<LineBreak/>", name, tabs, description);
							if (!string.IsNullOrEmpty(usage))
								commandHelpHint += string.Format("\t<Span Foreground=\"Gray\">{0}</Span><LineBreak/><LineBreak/>", usage);
						}
						else
						{
							commandHelpHint = string.Format("<Bold>{0}</Bold>{1}{2}", name, tabs, description);
							if (!string.IsNullOrEmpty(usage))
								commandHelpHint += string.Format("<LineBreak/>\t<Span Foreground=\"Gray\">{0}</Span>", usage);
						}
						if (name.StartsWith(command) && !string.IsNullOrEmpty(command))
							commandHelpHint = "<Span Foreground=\"Yellow\">" + commandHelpHint + "</Span>";
						commandHelpHints += commandHelpHint;
					}
				}
			else
			{
				commandHelpHints = InternalCommands.GoogleTranslate.GetAttribute<CommandUsageAttribute>()?.Usage;
			}
			string docSource = string.Format("<Paragraph xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xml:space=\"preserve\">{0}</Paragraph>", commandHelpHints);
			Paragraph obj = (Paragraph)XamlReader.Parse(docSource);
			FlowDocumentHelp.Blocks.Add(obj);
		}

		private void Window_Deactivated(object sender, EventArgs e)
		{
			Hide();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
				Hide();
            if (e.Key == Key.Up && sender is TextBox)
                SearchText = CommandHistory[0];
            if (e.Key == Key.Down && sender is TextBox)
            {
                int index = CommandHistory.IndexOf(SearchText);
                SearchText = index == 0 ? CommandHistory[0] : CommandHistory[index + 1];
            }                
        }

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			CommandRecognition();
			if (e.Key == Key.Enter)
			{
				Hide();
				ProcessSearch();
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - ActualWidth / 2;
			Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - ActualHeight / 2 - (System.Windows.SystemParameters.PrimaryScreenHeight / 2) / 2;
			TextBoxSearch.Text = null;
		}

        #endregion

        #region Constructor

        public MainWindow()
		{
			Instance = this;
            CommandHistory = new List<string>();
			DataContext = this;
			InitializeComponent();
			// Load settings...

			// Create an keyboard hook...
			KeyboardHook = new KeyboardHook();
			KeyboardHook.KeyPressed += (object sender, KeyPressedEventArgs e) =>
			{
				Show();
				Activate();
				TextBoxSearch.Focus();
			};
			KeyboardHook.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Windows, System.Windows.Forms.Keys.Space); // Default hotkey.
			
			// Hide window and help section...
			Visibility = Visibility.Hidden;
			BorderHelp.Visibility = Visibility.Collapsed;

			App.AutoStartOnLogon = true;
		}

		#endregion
	}
}
