using IdeaAnchor.Database;
using IdeaAnchor.Helper;
using IdeaAnchor.ViewModels;
using Microsoft.Maui.Platform;
using static System.Net.Mime.MediaTypeNames;

namespace IdeaAnchor.Pages;

public partial class IdeaPage : ContentPage
{
    private IdeaViewModel _vm => BindingContext as IdeaViewModel;

	public IdeaPage(IdeaViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

        Init();
    }

    private async void Init()
    {
        await Task.Delay(50);

        //only focus editor for new notes
        if (_vm.ExistingIdea == null)
            FocusEditor();

        if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.macOS)
        {
            EntryTitle.Margin = new Thickness(11, 0, 11, 0);
        }
    }

    private void FocusEditor()
    {
        try
        {
            EditorMain.Focus();

            if (EditorMain.Text != null)
                EditorMain.CursorPosition = EditorMain.Text.Length;

            KeyboardHelper.KeyboardVisibility(visible: true);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void Container_Tapped(System.Object sender, System.EventArgs e)
    {
        FocusEditor();
    }

    private void SaveIdea(System.Object sender, System.EventArgs e)
    {
        _ = _vm.SaveIdea();
    }

    private async void GoBack(System.Object sender, System.EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteIdea(System.Object sender, System.EventArgs e)
    {
        var result = await DisplayAlert("", "Are you sure you want to delete this idea?", "Yes", "No");
        if (!result)
            return;

        await _vm.DeleteIdea();

        GoBack(null, null);
    }

    private async void ShareIdea(System.Object sender, System.EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = _vm.IdeaTitle,
            Text = _vm.IdeaContent,
        });
    }
}
