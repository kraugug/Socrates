using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socrates
{
	public class CommandDescriptionAttribute : Attribute
	{
		#region Properties

		public string Description { get; }

		#endregion

		#region Constructor

		public CommandDescriptionAttribute(string description)
		{
			Description = description;
		}

		#endregion
	}
}
