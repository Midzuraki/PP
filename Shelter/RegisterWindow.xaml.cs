using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace AppliedProgramming
{
    public partial class RegisterWindow : Window, IBackWindow
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void pbPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pbPass.Tag = pbPass.Password;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text.Trim();
            string phone = tbPhone.Text.Trim();
            string login = tbLogin.Text.Trim();
            string pass = pbPass.Password;

            if (name == "" || phone == "" || login == "" || pass == "" || cbRole.SelectedIndex < 0)
            {
                Warn("Заполните все поля формы.");
                return;
            }
            if (name.Length < 5 || !name.Contains(" "))
            {
                Warn("Укажите ФИО полностью (фамилия и имя через пробел).");
                return;
            }
            if (!Input.IsValidPhone(phone))
            {
                Warn("Номер телефона должен содержать не менее 5 цифр.");
                return;
            }
            if (login.Length < 5 || !Regex.IsMatch(login, "^[A-Za-z0-9_]+$"))
            {
                Warn("Логин: не менее 5 символов, только латинские буквы, цифры и «_».");
                return;
            }
            if (Db.LoginExists(login))
            {
                Warn("Пользователь с таким логином уже существует.");
                return;
            }
            if (pass.Length < 5)
            {
                Warn("Пароль должен содержать не менее 5 символов.");
                return;
            }

            Role role = Role.Admin;
            switch (cbRole.SelectedIndex)
            {
                case 1: role = Role.Volunteer; break;
                case 2: role = Role.Vet; break;
            }

            Db.Users.Add(new User { FullName = name, Phone = phone, Login = login, Password = pass, Role = role });

            MessageBox.Show("Регистрация прошла успешно. Теперь можно войти в систему.", "Регистрация",
                MessageBoxButton.OK, MessageBoxImage.Information);
            Nav.Go(this, new LoginWindow());
        }

        private static void Warn(string text)
        {
            MessageBox.Show(text, "Регистрация", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        public void GoBack()
        {
            Nav.Go(this, new LoginWindow());
        }
    }
}
