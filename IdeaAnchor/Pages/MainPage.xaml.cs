using IdeaAnchor.Pages;
using IdeaAnchor.ViewModels;
using IdeaAnchor.Helper;

namespace IdeaAnchor;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        BorderMyIdeas.BackgroundColor = ThemeColors.ColorDarkGrey;
        BorderNewIdea.BackgroundColor = ThemeColors.ColorDarkGrey;
    }

    private async void GoToMyIdeas(object sender, EventArgs e)
	{
		BorderMyIdeas.BackgroundColor = ThemeColors.Primary;

        await Shell.Current.GoToAsync(nameof(IdeasListPage));
    }

    private async void CreateNewIdea(object sender, EventArgs e)
    {
        BorderNewIdea.BackgroundColor = ThemeColors.Primary;
        await Shell.Current.GoToAsync(nameof(IdeaPage));
    }
}


