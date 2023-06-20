using IdeaAnchor.Pages;
using IdeaAnchor.ViewModels;

namespace IdeaAnchor;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

	private async void GoToMyIdeas(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(IdeasListPage));
    }

    private async void CreateNewIdea(object sender, EventArgs e)
    {
		//navigate to idea page
		await Shell.Current.GoToAsync(nameof(IdeaPage));
    }
}


