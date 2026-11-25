using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppliedProgramming
{
    public partial class RequestWindow : Window, IBackWindow
    {
        private const string Give = "Сдать животное в приют";
        private const string Take = "Взять животное в семью";

        private readonly string login;
        private readonly string password;

        public RequestWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            userBar.SetUser(login, password);

            cbType.ItemsSource = new[] { Give, Take };
            cbKind.ItemsSource = Db.Kinds;
            cbPet.ItemsSource = Db.Pets.Where(p => !p.IsAdopted).ToList();
            tbDate.Text = DateTime.Today.ToString("dd.MM.yyyy");
            cbType.SelectedIndex = 0;

            // Список заявок видят только сотрудники
            if (Db.RoleOf(login) == Role.Client)
            {
                colRequests.Width = new GridLength(0);
                Width = 480;
            }
            else
            {
                dgRequests.ItemsSource = Db.Requests;
            }
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool give = cbType.SelectedItem as string == Give;
            spGive.Visibility = give ? Visibility.Visible : Visibility.Collapsed;
            spTake.Visibility = give ? Visibility.Collapsed : Visibility.Visible;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string name = tbClient.Text.Trim();
            string phone = tbPhone.Text.Trim();
            string info;

            if (name == "" || phone == "")
            {
                Warn("Укажите ФИО и номер телефона.");
                return;
            }
            if (!Input.IsValidPhone(phone))
            {
                Warn("Номер телефона должен содержать не менее 5 цифр.");
                return;
            }

            if (cbType.SelectedItem as string == Give)
            {
                double age;
                if (tbPetName.Text.Trim() == "" || cbKind.SelectedIndex < 0 || tbBreed.Text.Trim() == "")
                {
                    Warn("Заполните кличку, вид и породу питомца.");
                    return;
                }
                if (!Input.TryParseDouble(tbAge.Text, out age) || age < 0 || age > 40)
                {
                    Warn("Возраст должен быть числом от 0 до 40.");
                    return;
                }
                info = tbPetName.Text.Trim() + ", " + cbKind.SelectedItem + ", " + tbBreed.Text.Trim() + ", " + age + " л.";
            }
            else
            {
                Pet pet = cbPet.SelectedItem as Pet;
                if (pet == null)
                {
                    Warn("Выберите питомца из списка.");
                    return;
                }
                info = pet.Display;
            }

            Db.Requests.Add(new PetRequest
            {
                ClientName = name,
                Phone = phone,
                RequestType = (string)cbType.SelectedItem,
                PetInfo = info,
                Date = DateTime.Today
            });

            MessageBox.Show("Заявка принята. Наш сотрудник свяжется с вами по телефону.", "Заявка",
                MessageBoxButton.OK, MessageBoxImage.Information);

            tbClient.Clear(); tbPhone.Clear(); tbPetName.Clear(); tbBreed.Clear(); tbAge.Clear();
        }

        private static void Warn(string text)
        {
            MessageBox.Show(text, "Заявка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        public void GoBack()
        {
            Nav.Go(this, new MenuWindow(login, password));
        }
    }
}
