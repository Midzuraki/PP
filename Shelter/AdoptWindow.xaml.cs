using System;
using System.Linq;
using System.Windows;

namespace AppliedProgramming
{
    public partial class AdoptWindow : Window, IBackWindow
    {
        private readonly string login;
        private readonly string password;

        public AdoptWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            userBar.SetUser(login, password);
            cbPet.ItemsSource = Db.Pets.Where(p => !p.IsAdopted).ToList();
        }

        private void btnAdopt_Click(object sender, RoutedEventArgs e)
        {
            Pet pet = cbPet.SelectedItem as Pet;
            string owner = tbOwner.Text.Trim();
            string phone = tbPhone.Text.Trim();

            if (pet == null || owner == "" || phone == "")
            {
                Warn("Выберите питомца и укажите ФИО и телефон владельца.");
                return;
            }
            if (!Input.IsValidPhone(phone))
            {
                Warn("Номер телефона должен содержать не менее 5 цифр.");
                return;
            }

            pet.IsAdopted = true;
            Db.Adoptions.Add(new Adoption
            {
                PetNumber = pet.Number,
                PetName = pet.Name,
                OwnerName = owner,
                OwnerPhone = phone,
                Date = DateTime.Today
            });

            MessageBox.Show("Питомец «" + pet.Name + "» передан в семью. В журнал учёта внесена отметка о выбытии.",
                "Выдача питомца", MessageBoxButton.OK, MessageBoxImage.Information);
            GoBack();
        }

        private static void Warn(string text)
        {
            MessageBox.Show(text, "Выдача питомца", MessageBoxButton.OK, MessageBoxImage.Warning);
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
