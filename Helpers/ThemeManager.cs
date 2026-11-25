using System;
using System.Windows;

namespace AppliedProgramming
{
    // Переключение тем приложения (ПР №11).
    // Словарь темы всегда лежит первым в MergedDictionaries приложения.
    public static class ThemeManager
    {
        private static readonly string[] files = { "DefaultTheme", "BlueTheme", "DarkTheme" };
        private static readonly string[] names = { "Обычная", "Монохром (синяя)", "Тёмная" };
        private static int index = 0;

        public static string CurrentName
        {
            get { return names[index]; }
        }

        public static string ButtonText
        {
            get { return "Тема: " + names[index]; }
        }

        public static void Next()
        {
            index = (index + 1) % files.Length;

            ResourceDictionary theme = new ResourceDictionary();
            theme.Source = new Uri("Themes/" + files[index] + ".xaml", UriKind.Relative);

            Application.Current.Resources.MergedDictionaries[0] = theme;
        }
    }
}
