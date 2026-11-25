using System;
using System.Linq;
using System.Windows;

namespace AppliedProgramming
{
    public partial class VetWindow : Window, IBackWindow
    {
        private readonly string login;
        private readonly string password;

        public VetWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            userBar.SetUser(login, password);

            dpDate.SelectedDate = DateTime.Today;
            cbProcedure.ItemsSource = new[]
            {
                "Осмотр", "Вакцинация", "Стерилизация", "Лечение",
                "Обработка от паразитов", "Чипирование", "Другое"
            };
            cbPet.ItemsSource = Db.Pets.Where(p => !p.IsAdopted).ToList();
            dgRecords.ItemsSource = Db.VetRecords;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Pet pet = cbPet.SelectedItem as Pet;

            if (dpDate.SelectedDate == null || cbProcedure.SelectedIndex < 0 || pet == null)
            {
                MessageBox.Show("Укажите дату, вид манипуляции и питомца.", "Ветеринария",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (dpDate.SelectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Дата манипуляции не может быть в будущем.", "Ветеринария",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Db.VetRecords.Add(new VetRecord
            {
                Date = dpDate.SelectedDate.Value,
                Procedure = (string)cbProcedure.SelectedItem,
                PetNumber = pet.Number,
                PetName = pet.Name,
                Notes = tbNotes.Text.Trim()
            });
            tbNotes.Clear();
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
