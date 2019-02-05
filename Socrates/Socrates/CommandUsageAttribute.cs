using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socrates
{
	public class CommandUsageAttribute : Attribute
	{
		#region Properties

		public bool AddTheWord { get; }

		public string Usage { get; }

		#endregion

		#region Constructor

		public CommandUsageAttribute(string usage, bool addTheWord = true)
		{
			Usage = addTheWord ? "Usage: " + usage : usage;
		}

		#endregion
	}
}
