using Microsoft.Maui.Platform;
using IdeaAnchor.Helper;

namespace IdeaAnchor;

public partial class App : Application
{
	public App(AppShell appShell)
	{
		InitializeComponent();

        SetUpEditor();

        SetUpEntry();

        MainPage = appShell;
    }

    /// <summary>
    /// Set up the colors for the Entry control
    /// </summary>
    private void SetUpEntry()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("SetUpEntry", (handler, view) =>
        {
#if ANDROID
            //set cursor color
            handler.PlatformView.TextCursorDrawable.SetTint(ThemeColors.Primary.ToPlatform());

            //set highlight color
            handler.PlatformView.SetHighlightColor(ThemeColors.Primary.ToPlatform());

            //remove underline
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());

#elif IOS || MACCATALYST
         
#elif WINDOWS
      
#endif
        });
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

                //set cursor color
                handler.PlatformView.TextCursorDrawable.SetTint(primaryColor.ToPlatform());

                //set highlight color
                handler.PlatformView.SetHighlightColor(primaryColor.ToPlatform());

                //remove underline
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
            }

#elif IOS || MACCATALYST
         
#elif WINDOWS
      
#endif
        });
    }
}

