using System.Windows;
using Microsoft.Win32;

namespace AppliedProgramming
{
    public partial class AddPetWindow : Window, IBackWindow
    {
        private readonly string login;
        private readonly string password;

        public AddPetWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            userBar.SetUser(login, password);

            cbKind.ItemsSource = Db.Kinds;
            cbGender.ItemsSource = Db.Genders;
            cbSize.ItemsSource = Db.Sizes;
        }

        private void btnPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";
            if (dlg.ShowDialog() == true)
            {
                tbPhoto.Text = dlg.FileName;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            double age, weight;

            if (tbName.Text.Trim() == "" || tbBreed.Text.Trim() == "" || tbColor.Text.Trim() == ""
                || cbKind.SelectedIndex < 0 || cbGender.SelectedIndex < 0 || cbSize.SelectedIndex < 0)
            {
                Warn("Заполните кличку, вид, пол, породу, окрас и размер.");
                return;
            }
            if (!Input.TryParseDouble(tbAge.Text, out age) || age < 0 || age > 40)
            {
                Warn("Возраст должен быть числом от 0 до 40.");
                return;
            }
            if (!Input.TryParseDouble(tbWeight.Text, out weight) || weight <= 0 || weight > 200)
            {
                Warn("Вес должен быть числом больше 0 и не более 200 кг.");
                return;
            }

            Pet pet = new Pet
            {
                Number = Db.NextPetNumber(),
                Name = tbName.Text.Trim(),
                Kind = (string)cbKind.SelectedItem,
                Gender = (string)cbGender.SelectedItem,
                Breed = tbBreed.Text.Trim(),
                Color = tbColor.Text.Trim(),
                Size = (string)cbSize.SelectedItem,
                Age = age,
                Weight = weight,
                Vaccines = tbVaccines.Text.Trim() == "" ? "Нет данных" : tbVaccines.Text.Trim(),
                PhotoPath = tbPhoto.Text
            };
            Db.Pets.Add(pet);

            MessageBox.Show("Питомец зарегистрирован под номером " + pet.Number + ".", "Регистрация питомца",
                MessageBoxButton.OK, MessageBoxImage.Information);
            GoBack();
        }

        private static void Warn(string text)
        {
            MessageBox.Show(text, "Регистрация питомца", MessageBoxButton.OK, MessageBoxImage.Warning);
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
