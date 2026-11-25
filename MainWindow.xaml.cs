using System.Windows;
using System.Windows.Controls;

namespace AppliedProgramming
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnTheme.Content = ThemeManager.ButtonText;
        }

        // Каждая кнопка запускает свою практическую работу в отдельном окне
        private void Pr_Click(object sender, RoutedEventArgs e)
        {
            Window w = null;

            switch (int.Parse((string)((Button)sender).Tag))
            {
                case 1: w = new CalculatorWindow(); break;
                case 2: w = new CardWindow(); break;
                case 3: w = new Pr3Window(); break;
                case 4: w = new InterfaceWindow(); break;

                // ПР 5-10: система «В добрые руки», начинается с окна авторизации
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    w = new LoginWindow();
                    break;

                // ПР 11: главное окно системы с кнопкой смены темы
                case 11: w = new MenuWindow("admin", "admin123"); break;
            }

            if (w != null) w.Show();
        }

        private void btnTheme_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.Next();
            btnTheme.Content = ThemeManager.ButtonText;
        }
    }
}
