using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Socrates
{
	public static class InternalCommandsExtensions
	{
		#region Methods

		public static bool ContainsParameter(this InternalCommands command, string parameter)
		{
			CommandParametersAttribute attribute = GetAttribute<CommandParametersAttribute>(command);
			return attribute != null ? attribute.Parameters.Contains(parameter) : false;
		}

		public static TAttribute GetAttribute<TAttribute>(this InternalCommands command)
		{
			MemberInfo member = typeof(InternalCommands).GetMember(command.ToString()).FirstOrDefault();
			object attribute = member?.GetCustomAttributes(typeof(TAttribute), false)?.FirstOrDefault();
			return (TAttribute)attribute;
		}

		#endregion
	}
}
