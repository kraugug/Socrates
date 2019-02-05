using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socrates
{
	public class CommandHelpAttribute : Attribute
	{
		#region Properties

		public string Help { get; }

		#endregion

		#region Constructor

		public CommandHelpAttribute(string help)
		{
			Help = help;
		}

		#endregion
	}
}
