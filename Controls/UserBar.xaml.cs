using System.Windows;
using System.Windows.Controls;

namespace AppliedProgramming
{
    // Логин и пароль в правом верхнем углу (ПР №5, №6)
    public partial class UserBar : UserControl
    {
        public UserBar()
        {
            InitializeComponent();
        }

        public void SetUser(string login, string password)
        {
            miUser.Header = login + " ▾";
            tbPassword.Text = string.IsNullOrEmpty(password)
                ? ""
                : "Пароль: " + new string('•', password.Length);
        }

        // Единственный пункт списка: завершает работу приложения
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
