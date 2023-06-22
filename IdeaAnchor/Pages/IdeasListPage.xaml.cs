using IdeaAnchor.ViewModels;
using IdeaAnchor.ItemViewModels;

namespace IdeaAnchor.Pages;

public partial class IdeasListPage : ContentPage
{
    private IdeasListViewModel _vm => BindingContext as IdeasListViewModel;

    public IdeasListPage(IdeasListViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _ = _vm.LoadIdeas();
    }

    private async void ListView_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        var selectedIdea = (e.SelectedItem as IdeaItemViewModel).Idea;

        var navigationParameter = new Dictionary<string, object> { { "ExistingIdea", selectedIdea } };
        await Shell.Current.GoToAsync(nameof(IdeaPage), navigationParameter);

        //clear selection so the tap event will work next time
        ((ListView)sender).SelectedItem = null;
    }

    private async void GoBack(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
