using System;

namespace IdeaAnchor.Helper
{
	public static class ThemeColors
	{
        public static Color Primary => GetColor("Primary");

        public static Color ColorMidGrey => GetColor("ColorMidGrey");

        public static Color ColorDarkGrey => GetColor("ColorDarkGrey");

        public static Color ColorVeryDarkGrey => GetColor("ColorVeryDarkGrey");

        private static Color GetColor(string colorName)
		{
            if (App.Current.Resources.TryGetValue(colorName, out var colorValue))
                return colorValue as Color;

			return Colors.Black;
        }
	}
}

