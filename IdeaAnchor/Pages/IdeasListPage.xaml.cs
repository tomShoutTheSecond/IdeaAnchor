using IdeaAnchor.ViewModels;

namespace IdeaAnchor.Pages;

public partial class IdeasListPage : ContentPage
{
	public IdeasListPage(IdeasListViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}
