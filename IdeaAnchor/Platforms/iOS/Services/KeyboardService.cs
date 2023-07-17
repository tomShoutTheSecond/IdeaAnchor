using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace IdeaAnchor.Services
{
    /// <summary>
    /// iOS KeyboardService
    /// </summary>
    public partial class KeyboardService
    {
        private NSObject notification;

        public partial void Init()
        {
            notification = UIKeyboard.Notifications.ObserveDidShow((sender, args) =>
            {
                KeyboardDidOpen?.Invoke(this, GetWindowHeight(args.FrameEnd, isKeyboardOpening: true));
            });

            notification = UIKeyboard.Notifications.ObserveDidHide((sender, args) =>
            {
                KeyboardDidClose?.Invoke(this, GetWindowHeight(args.FrameEnd, isKeyboardOpening: false));
            });
        }

        public partial float GetInitialWindowHeight()
        {
            return GetWindowHeight(UIScreen.MainScreen.Bounds, false);
        }

        private float GetWindowHeight(CGRect frameEnd, bool isKeyboardOpening)
        {
            var bottomSafeAreaHeight = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom;
            var extraHeight = isKeyboardOpening && bottomSafeAreaHeight > 0 ? 6 : 0;
            var statusBarHeight = UIApplication.SharedApplication.StatusBarFrame.Height + extraHeight;
            var windowHeight = UIScreen.MainScreen.Bounds.Height - statusBarHeight;
            if (isKeyboardOpening)
            {
                windowHeight -= frameEnd.Height;
            }
            else
            {
                windowHeight -= bottomSafeAreaHeight;
            }

            return (float)windowHeight;
        }
    }
}