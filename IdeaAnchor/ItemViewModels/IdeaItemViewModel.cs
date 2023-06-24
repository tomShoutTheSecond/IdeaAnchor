using System;
using IdeaAnchor.Helper;

namespace IdeaAnchor.ItemViewModels
{
	public class IdeaItemViewModel
	{
		public Models.Idea Idea { get; set; }

		public bool IsSelected { get; set; }

		public DateTime LastUpdatedDateTime => Idea.LastUpdatedTime.ToDateTime();
	}
}

