using IdeaAnchor.Pages;
using IdeaAnchor.ViewModels;
using IdeaAnchor.Helper;

namespace IdeaAnchor;

public partial class MainPage : ContentPage
{
	private MainViewModel _vm => BindingContext as MainViewModel;

	public MainPage(MainViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		_vm.OnAppearing();
    }
}


