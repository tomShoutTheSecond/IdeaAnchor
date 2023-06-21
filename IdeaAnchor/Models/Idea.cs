using System;
using SQLite;
using IdeaAnchor.Helper;

namespace IdeaAnchor.Models
{
	public class Idea
	{
		[PrimaryKey]
		public string Id { get; set; }

		public string Content { get; set; }

		public string Title { get; set; }
	}
}

