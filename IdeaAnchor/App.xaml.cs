using Microsoft.Maui.Platform;

namespace IdeaAnchor;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        SetUpEditor();

        MainPage = new AppShell();
	}

    /// <summary>
    /// Set up the colors for the Editor control
    /// </summary>
    private void SetUpEditor()
    {
        Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("SetUpEditor", (handler, view) =>
        {
#if ANDROID
            //set the cursor color to Primary from Colors.xaml
            if (App.Current.Resources.TryGetValue("Primary", out var color))
            {
                var primaryColor = (Color)color;
                handler.PlatformView.TextCursorDrawable.SetTint(primaryColor.ToPlatform());
                handler.PlatformView.SetHighlightColor(primaryColor.ToPlatform());
            }

#elif IOS || MACCATALYST
         
#elif WINDOWS
      
#endif
        });
    }
}

