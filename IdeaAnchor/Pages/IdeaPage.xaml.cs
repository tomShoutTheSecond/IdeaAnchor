using CommunityToolkit.Maui.Core.Platform;
using IdeaAnchor.Services;
using IdeaAnchor.ViewModels;

namespace IdeaAnchor.Pages;

public partial class IdeaPage : ContentPage
{
    private const string LabelSavedAnimationKey = "LabelSavedAnimation";

    private IdeaViewModel _vm => BindingContext as IdeaViewModel;

    private readonly KeyboardService _keyboardService;

    public IdeaPage(IdeaViewModel vm,
        KeyboardService keyboardService)
	{
        _keyboardService = keyboardService;

		InitializeComponent();

        BindingContext = vm;

        Init();
    }

    private async void Init()
    {
        _vm.SavedEvent += (s, e) => AnimateSavedLabel();

        SetTitleAlignment();

        var openKeyboardDelay = DeviceInfo.Platform == DevicePlatform.iOS ? 500 : 50;
        await Task.Delay(openKeyboardDelay);

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

#if !MACCATALYST
            EditorMain.ShowKeyboardAsync(CancellationToken.None);
#endif
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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        SetupKeyboardWindowResize();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            _keyboardService.KeyboardDidOpen -= ResizeWindowForKeyboard;
            _keyboardService.KeyboardDidClose -= ResizeWindowForKeyboard;
        }
    }

    private void SetupKeyboardWindowResize()
    {
        if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            //set initial window height
            ResizeWindowForKeyboard(this, _keyboardService.GetInitialWindowHeight());

            //listen for keyboard changes
            _keyboardService.KeyboardDidOpen += ResizeWindowForKeyboard;
            _keyboardService.KeyboardDidClose += ResizeWindowForKeyboard;
        }
        else if (DeviceInfo.Platform == DevicePlatform.Android
            || DeviceInfo.Platform == DevicePlatform.macOS)
        {
            MainGrid.VerticalOptions = LayoutOptions.Fill;
        }
    }

    private void ResizeWindowForKeyboard(object sender, float endHeight)
    {
        MainGrid.HeightRequest = endHeight;
    }
}
