using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socrates
{
	public class CommandNameAttribute : Attribute
	{
		#region Constants

		public const char Delimiter = ';';

		#endregion

		#region Properties

		public List<string> Commands { get; }

		#endregion

		#region Constructor

		public CommandNameAttribute(string commands) : this(commands.Split(new char[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries))
		{ }

		public CommandNameAttribute(params string[] commands)
		{
			Commands = commands.ToList();
		}

		#endregion
	}
}
