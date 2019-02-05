using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socrates
{
	public class CommandParametersAttribute : Attribute
	{
		#region Constants

		public const char Delimiter = ';';

		#endregion

		#region Properties

		public List<string> Parameters { get; }

		#endregion

		#region Constructor

		public CommandParametersAttribute(string parameters) : this(parameters.Split(new char[] { Delimiter }, StringSplitOptions.RemoveEmptyEntries))
		{ }

		public CommandParametersAttribute(params string[] parameters)
		{
			Parameters = parameters.ToList();
		}

		#endregion
	}
}
