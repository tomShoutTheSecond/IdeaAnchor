using System;
using Microsoft.Maui.Platform;

namespace IdeaAnchor.Helper
{
	public static class KeyboardHelper
	{
        public static async void KeyboardVisibility(bool visible)
        {
#if ANDROID
            try
            {
                await Task.Delay(50);

                if (Platform.CurrentActivity.CurrentFocus != null)
                {
                    if (visible)
                    {
                        Platform.CurrentActivity.ShowKeyboard(Platform.CurrentActivity.CurrentFocus);
                    }
                    else
                    {
                        Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
                        Platform.CurrentActivity.CurrentFocus.ClearFocus();
                    }
                }
            }
            catch
            {

            }
#endif
        }
    }
}

