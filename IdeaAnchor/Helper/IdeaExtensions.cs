using System;
using IdeaAnchor.Models;

namespace IdeaAnchor.Helper
{
	public static class IdeaExtensions
	{
		public static string GetTitle(this Idea idea)
		{
			try
			{
                var trimmedContent = idea.Content.Trim();

                var indexOfFirstLineBreak = trimmedContent.IndexOf("\n");

				if (indexOfFirstLineBreak == -1)
					return trimmedContent;

                var firstLine = trimmedContent.Substring(0, indexOfFirstLineBreak);

                //TODO: limit to max 10 words

                return firstLine;
            }
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}
	}
}

