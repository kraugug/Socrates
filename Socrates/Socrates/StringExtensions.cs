using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socrates
{
	public static class StringExtensions
	{
		#region Fields

		private static string[,] GoogleLanguageCodes = new string[104, 2] {
			{ "Afrikaans", "af" },
			{ "Albanian", "sq" },
			{ "Amharic", "am" },
			{ "Arabic", "ar" },
			{ "Armenian", "hy" },
			{ "Azerbaijani", "az" },
			{ "Basque", "eu" },
			{ "Belarusian", "be" },
			{ "Bengali", "bn" },
			{ "Bosnian", "bs" },
			{ "Bulgarian", "bg" },
			{ "Catalan", "ca" },
			{ "Cebuano (ISO-639-2)", "ceb" },
			{ "Chinese (Simplified, BCP-47)", "zh-CN" },
			{ "Chinese (Traditional, BCP-47)", "zh-TW" },
			{ "Corsican", "co" },
			{ "Croatian", "hr" },
			{ "Czech", "cs" },
			{ "Danish", "da" },
			{ "Dutch", "nl" },
			{ "English", "en" },
			{ "Esperanto", "eo" },
			{ "Estonian", "et" },
			{ "Finnish", "fi" },
			{ "French", "fr" },
			{ "Frisian", "fy" },
			{ "Galician", "gl" },
			{ "Georgian", "ka" },
			{ "German", "de" },
			{ "Greek", "el" },
			{ "Gujarati", "gu" },
			{ "Haitian Creole", "ht" },
			{ "Hausa", "ha" },
			{ "Hawaiian (ISO-639-2)", "haw" },
			{ "Hebrew", "iw" },
			{ "Hindi", "hi" },
			{ "Hmong (ISO-639-2)", "hmn" },
			{ "Hungarian", "hu" },
			{ "Icelandic", "is" },
			{ "Igbo", "ig" },
			{ "Indonesian", "id" },
			{ "Irish", "ga" },
			{ "Italian", "it" },
			{ "Japanese", "ja" },
			{ "Javanese", "jw" },
			{ "Kannada", "kn" },
			{ "Kazakh", "kk" },
			{ "Khmer", "km" },
			{ "Korean", "ko" },
			{ "Kurdish", "ku" },
			{ "Kyrgyz", "ky" },
			{ "Lao", "lo" },
			{ "Latin", "la" },
			{ "Latvian", "lv" },
			{ "Lithuanian", "lt" },
			{ "Luxembourgish", "lb" },
			{ "Macedonian", "mk" },
			{ "Malagasy", "mg" },
			{ "Malay", "ms" },
			{ "Malayalam", "ml" },
			{ "Maltese", "mt" },
			{ "Maori", "mi" },
			{ "Marathi", "mr" },
			{ "Mongolian", "mn" },
			{ "Myanmar (Burmese)", "my" },
			{ "Nepali", "ne" },
			{ "Norwegian", "no" },
			{ "Nyanja (Chichewa)", "ny" },
			{ "Pashto", "ps" },
			{ "Persian", "fa" },
			{ "Polish", "pl" },
			{ "Portuguese (Portugal, Brazil)", "pt" },
			{ "Punjabi", "pa" },
			{ "Romanian", "ro" },
			{ "Russian", "ru" },
			{ "Samoan", "sm" },
			{ "Scots Gaelic", "gd" },
			{ "Serbian", "sr" },
			{ "Sesotho", "st" },
			{ "Shona", "sn" },
			{ "Sindhi", "sd" },
			{ "Sinhala (Sinhalese)", "si" },
			{ "Slovak", "sk" },
			{ "Slovenian", "sl" },
			{ "Somali", "so" },
			{ "Spanish", "es" },
			{ "Sundanese", "su" },
			{ "Swahili", "sw" },
			{ "Swedish", "sv" },
			{ "Tagalog (Filipino)", "tl" },
			{ "Tajik", "tg" },
			{ "Tamil", "ta" },
			{ "Telugu", "te" },
			{ "Thai", "th" },
			{ "Turkish", "tr" },
			{ "Ukrainian", "uk" },
			{ "Urdu", "ur" },
			{ "Uzbek", "uz" },
			{ "Vietnamese", "vi" },
			{ "Welsh", "cy" },
			{ "Xhosa", "xh" },
			{ "Yiddish", "yi" },
			{ "Yoruba", "yo" },
			{ "Zulu", "zu" }
		};
		//private static string[] GoogleLanguageCodes = { "af", "ach", "ak", "am", "ar", "az", "be", "bem", "bg", "bh", "bn", "br", "bs", "ca", "chr", "ckb", "co", "crs", "cs", "cy", "da", "de",
		//	"ee", "el", "en", "eo", "es", "es-419", "et", "eu", "fa", "fi", "fo", "fr", "fy", "ga", "gaa", "gd", "gl", "gn", "gu", "ha", "haw", "hi", "hr", "ht", "hu", "hy", "ia", "id", "ig",
		//	"is", "it", "iw", "ja", "jw", "ka", "kg", "kk", "km", "kn", "ko", "kri", "ku", "ky", "la", "lg", "ln", "lo", "loz", "lt", "lua", "lv", "mfe", "mg", "mi", "mk", "ml", "mn", "mo",
		//	"mr", "ms", "mt", "ne", "nl", "nn", "no", "nso", "ny", "nyn", "oc", "om", "or", "pa", "pcm", "pl", "ps", "pt-BR", "pt-PT", "qu", "rm", "rn", "ro", "ru", "rw", "sd", "sh", "si", "sk",
		//	"sl", "sn", "so", "sq", "sr", "sr-ME", "st", "su", "sv", "sw", "ta", "te", "tg", "th", "ti", "tk", "tl", "tn", "to", "tr", "tt", "tum", "tw", "ug", "uk", "ur", "uz", "vi", "wo", "xh",
		//	"xx-bork", "xx-elmer", "xx-hacker", "xx-klingon", "xx-pirate", "yi", "yo", "zh-CN", "zh-TW", "zu" };
		private static string[] UrlReservedCharacters = { "!", "#", "$", "&", "'", "(", ")", "*", "+", ",", "/", ":", ";", "=", "?", "@", "[", "]" };
		private static string[] UrlReservedCharactersReplacement = { "%21", "%23", "%24", "%26", "%27", "%28", "%29", "%2A", "%2B", "%2C", "%2F", "%3A", "%3B", "%3D", "%3F", "%40", "%5B", "%5D" };

		#endregion

		#region Methods

		public static string[] ParseGoogleLanguages(this string str, string defaultLanguage = "auto")
		{
			string[] langs = null;
			foreach (string code in GoogleLanguageCodes)
				if (str.StartsWith(code))
				{
					langs = new string[2];
					if (code.Length == str.Length)
					{
						langs[0] = defaultLanguage;
						langs[1] = code;
					}
					else
					{
						langs[0] = code;
						langs[1] = str.Substring(code.Length);
						if (!GoogleLanguageCodes.Contains(langs[1], 1))
							throw new Exception(string.Format("Unrecognised Google language code '{0}'.", langs[1]));
					}
					break;
				}
			if (langs == null)
				throw new Exception(string.Format("Unrecognised Google language code '{0}'.", str));
			return langs;
		}

		public static bool Contains(this string[,] multiArray, string value, int index = 0)
		{
			for (int firstIndex = 0; firstIndex < multiArray.Length / multiArray.Rank; firstIndex++)
		 		if (multiArray[firstIndex, index].CompareTo(value) == 0)
					return true;
			return false;
		}

		public static string ParseUrl(this string str)
		{
			for (int index = 0; index < UrlReservedCharacters.Length; index++)
				str = str.Replace(UrlReservedCharacters[index], UrlReservedCharactersReplacement[index]);
			return str;
		}

		public static TCommandEnum ToCommand<TCommandEnum>(this string str)
		{
			try
			{
				Type enumType = typeof(TCommandEnum);
				Array enumValues = Enum.GetValues(enumType);
				foreach (Enum enumerator in enumValues)
				{
					CommandNameAttribute attribute = enumerator.GetAttribute<CommandNameAttribute>();
					if (attribute?.Commands.Where(c => c.CompareTo(str) == 0).Count() == 1)
						return (TCommandEnum)(object)enumerator;
				}
				return default(TCommandEnum);
			}
			catch
			{
				return default(TCommandEnum);
			}
		}

		#endregion
	}
}
