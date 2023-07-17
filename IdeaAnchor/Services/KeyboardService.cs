using System;

namespace IdeaAnchor.Services
{
    public partial class KeyboardService
    {
        public EventHandler<float> KeyboardDidOpen { get; set; }

        public EventHandler<float> KeyboardDidClose { get; set; }

        public KeyboardService()
        {
            Init();
        }

        public partial void Init();

        public partial float GetInitialWindowHeight();
    }
}