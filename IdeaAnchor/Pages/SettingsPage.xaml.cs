using IdeaAnchor.ViewModels;

namespace IdeaAnchor.Pages;

public partial class SettingsPage : ContentPage
{
    private SettingsViewModel _vm => BindingContext as SettingsViewModel;

    public SettingsPage(SettingsViewModel vm)
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
