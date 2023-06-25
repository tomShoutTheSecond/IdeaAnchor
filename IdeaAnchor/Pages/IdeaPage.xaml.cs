using CommunityToolkit.Maui.Core.Platform;
using IdeaAnchor.Database;
using IdeaAnchor.Helper;
using IdeaAnchor.ViewModels;
using Microsoft.Maui.Platform;
using static System.Net.Mime.MediaTypeNames;

namespace IdeaAnchor.Pages;

public partial class IdeaPage : ContentPage
{
    private const string LabelSavedAnimationKey = "LabelSavedAnimation";

    private IdeaViewModel _vm => BindingContext as IdeaViewModel;

	public IdeaPage(IdeaViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;

        Init();
    }

    private async void Init()
    {
        _vm.SavedEvent += (s, e) => AnimateSavedLabel();

        SetTitleAlignment();

        await Task.Delay(50);

        //only focus editor for new notes
        if (_vm.ExistingIdea == null)
            FocusEditor();
    }

    private void SetTitleAlignment()
    {
        if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.macOS)
        {
            EntryTitle.Margin = new Thickness(11, 0, 11, 0);
        }
        else if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            EntryTitle.Margin = new Thickness(6, 0, 6, 0);
        }
    }

    private void FocusEditor()
    {
        try
        {
            EditorMain.Focus();

            if (EditorMain.Text != null)
                EditorMain.CursorPosition = EditorMain.Text.Length;

            EditorMain.ShowKeyboardAsync(CancellationToken.None);
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

    private async void DeleteIdea(System.Object sender, System.EventArgs e)
    {
        var result = await DisplayAlert("", "Are you sure you want to delete this idea?", "Yes", "No");
        if (!result)
            return;

        await _vm.DeleteIdea();
    }

    private async void ShareIdea(System.Object sender, System.EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = "Share my idea",
            Text = $"{_vm.IdeaTitle}\n\n{_vm.IdeaContent}"
        });
    }

    private void AnimateSavedLabel()
    {
        LabelSaved.Animate(LabelSavedAnimationKey, new Animation((progress) => LabelSaved.Opacity = 1 - progress), rate: 16, length: 2000, Easing.SinIn);
    }
}
