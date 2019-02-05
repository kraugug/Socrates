using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Socrates
{
	public static class EnumExtensions
	{
		#region Methods

		public static bool Contains(this Enum enumeration, string name)
		{
			return Enum.Parse(enumeration.GetType(), name) != null;
		}

		public static bool ContainsCommand(this InternalCommands command)
		{
			return false;
		}

		public static TAttribute GetAttribute<TAttribute>(this Enum enumerator, bool inherit = false)
		{
			FieldInfo fieldInfo = enumerator.GetType().GetField(enumerator.ToString());
			if (fieldInfo != null)
			{
				object[] attrs = fieldInfo.GetCustomAttributes(typeof(TAttribute), inherit);
				if ((attrs != null) && (attrs.Length > 0))
				{
					return (TAttribute)attrs[0];
				}
			}
			return default(TAttribute);
		}

		#endregion
	}
}
