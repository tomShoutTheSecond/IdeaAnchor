using IdeaAnchor.Helper;
using IdeaAnchor.ViewModels;
using Microsoft.Maui.Platform;

namespace IdeaAnchor.Pages;

public partial class IdeaPage : ContentPage
{
    public IdeaViewModel ViewModel => BindingContext as IdeaViewModel;

	public IdeaPage()
	{
		InitializeComponent();

        BindingContext = new IdeaViewModel();

        Init();
    }

    private async void Init()
    {
        await Task.Delay(50);

        FocusEditor();
    }

    private void FocusEditor()
    {
        EditorMain.Focus();

        KeyboardHelper.KeyboardVisibility(visible: true);
    }

    private void Container_Tapped(System.Object sender, System.EventArgs e)
    {
        FocusEditor();
    }

    protected override bool OnBackButtonPressed()
    {
        ViewModel.SaveIdea();

        return base.OnBackButtonPressed();
    }
}
