using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace AppliedProgramming
{
    public partial class LoginWindow : Window, IBackWindow
    {
        private const int MaxAttempts = 3;
        private const int LockSeconds = 30;

        private int attempts = 0;
        private int secondsLeft = 0;
        private readonly DispatcherTimer timer = new DispatcherTimer();

        public LoginWindow()
        {
            InitializeComponent();
            btnTheme.Content = ThemeManager.ButtonText;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            Closed += (s, e) => timer.Stop();
        }

        private void pbPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // длина пароля нужна мультитриггеру стиля PasswordBox
            pbPass.Tag = pbPass.Password;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string pass = pbPass.Password;

            if (login == "" || pass == "")
            {
                MessageBox.Show("Заполните оба поля: логин и пароль.", "Авторизация",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(login, "^[A-Za-z0-9_]+$"))
            {
                MessageBox.Show("Логин может содержать только латинские буквы, цифры и знак «_».",
                    "Авторизация", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            User user = Db.FindUser(login, pass);
            if (user == null)
            {
                attempts++;
                if (attempts >= MaxAttempts)
                {
                    StartLock();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль. Осталось попыток: " + (MaxAttempts - attempts),
                        "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }

            attempts = 0;
            // логин и пароль передаются на следующее окно
            Nav.Go(this, new MenuWindow(user.Login, user.Password));
        }

        // Блокировка доступа на 30 секунд после третьей неудачной попытки
        private void StartLock()
        {
            secondsLeft = LockSeconds;
            SetInputsEnabled(false);
            tbInfo.Text = "Три неудачные попытки. Доступ заблокирован на " + secondsLeft + " с.";
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsLeft--;
            if (secondsLeft <= 0)
            {
                timer.Stop();
                attempts = 0;
                tbInfo.Text = "";
                SetInputsEnabled(true);
                tbLogin.Focus();
            }
            else
            {
                tbInfo.Text = "Три неудачные попытки. Доступ заблокирован на " + secondsLeft + " с.";
            }
        }

        private void SetInputsEnabled(bool enabled)
        {
            tbLogin.IsEnabled = enabled;
            pbPass.IsEnabled = enabled;
            btnLogin.IsEnabled = enabled;
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            Nav.Go(this, new MenuWindow(Db.ClientLogin, ""));
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Nav.Go(this, new RegisterWindow());
        }

        private void btnTheme_Click(object sender, RoutedEventArgs e)
        {
            ThemeManager.Next();
            btnTheme.Content = ThemeManager.ButtonText;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        public void GoBack()
        {
            Close();
        }
    }
}
