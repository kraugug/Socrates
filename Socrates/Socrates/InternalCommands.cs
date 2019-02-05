using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Socrates
{
	public enum InternalCommands
	{
		Unknown,

		[CommandName("az")]
		[CommandDescription("Opens link via AnonymZ")]
		[CommandUsage("az &lt;link&gt;")]
		AnonymZ,

		[CommandName("csfd")]
		[CommandDescription("Searches on CSFD (Czech-Slovak Film Database)")]
		[CommandUsage("csfd &lt;text to search&gt;")]
		Csfd,

		[CommandName("exit")]
		[CommandDescription("Closes the Socrates")]
		Exit,

		[CommandName("gtr")]
		[CommandDescription("Translates text via Google")]
		[CommandUsage("gtr [languages] &lt;text to translate&gt;<LineBreak/>\tExample: gtr enru Socrates")]
		GoogleTranslate,

		/// <summary>
		/// Search types:
		/// tt ... Titles
		/// ep ... TV Episodes
		/// nm ... Names
		/// co ... Companies
		/// kw ... Keywords
		/// </summary>
		[CommandName("imdb")]
		[CommandDescription("Searches on IMDb (Internet Movie Database)")]
		[CommandParameters("tt;ep;nm;co;kw")]
		[CommandUsage("imdb [tt|ep|nm|co|kw|] &lt;text to search&gt;<LineBreak/>\t\ttt\t...\tTitles<LineBreak/>\t\tep\t...\tTV Episodes<LineBreak/>\t\tnm\t...\tNames<LineBreak/>\t\tco\t...\tCompanies<LineBreak/>\t\tkw\t...\tKeywords")]
		Imdb,

		[CommandName("mtc")]
		[CommandDescription("Searches on Metacritic")]
		[CommandUsage("mtc &lt;text to search&gt;")]
		Metacritic,

		[CommandName("rtt")]
		[CommandDescription("Searches on Rotten Tomatoes")]
		[CommandUsage("rtt &lt;text to search&gt;")]
		RottenTomatoes,

		[CommandName("shutdown")]
		[CommandDescription("Power OFF the computer")]
		Shutdown,

		[CommandName("tvg")]
		[CommandDescription("TV guide (supports: Horror Channel)")]
		[CommandUsage("tvg &lt;channel name&gt;<LineBreak/>\tExample: tvg horror | tvg film 4")]
		TvGuide,

		[CommandName("wiki")]
		[CommandDescription("Searches on Wikipidea")]
		[CommandUsage("wiki [language] &lt;text to search&gt;")]
		Wikipedia,

		[CommandName("yt", "youtube")]
		[CommandDescription("Searches on YouTube")]
		[CommandUsage("yt &lt;text to search&gt;")]
		YouTube,

		Default = Unknown
	}
}
