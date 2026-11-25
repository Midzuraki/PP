using System.Linq;
using System.Windows;

namespace AppliedProgramming
{
    public partial class MenuWindow : Window, IBackWindow
    {
        private readonly string login;
        private readonly string password;

        public MenuWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;

            Role role = Db.RoleOf(login);
            DataContext = new Access(role);

            userBar.SetUser(login, password);
            tbWelcome.Text = "Здравствуйте, " + Db.NameOf(login) + "!";
            tbRole.Text = "Ваша роль: " + Access.RoleTitle(role);
            tbInShelter.Text = Db.Pets.Count(p => !p.IsAdopted).ToString();
            tbAdopted.Text = Db.Pets.Count(p => p.IsAdopted).ToString();
            tbRequests.Text = Db.Requests.Count.ToString();
            btnTheme.Content = ThemeManager.ButtonText;
        }

        private void btnPets_Click(object sender, RoutedEventArgs e) { Nav.Go(this, new PetsWindow(login, password)); }
        private void btnRequest_Click(object sender, RoutedEventArgs e) { Nav.Go(this, new RequestWindow(login, password)); }
        private void btnAddPet_Click(object sender, RoutedEventArgs e) { Nav.Go(this, new AddPetWindow(login, password)); }
        private void btnAdopt_Click(object sender, RoutedEventArgs e) { Nav.Go(this, new AdoptWindow(login, password)); }
        private void btnVet_Click(object sender, RoutedEventArgs e) { Nav.Go(this, new VetWindow(login, password)); }
        private void btnReport_Click(object sender, RoutedEventArgs e) { Nav.Go(this, new ReportWindow(login, password)); }

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
            Nav.Go(this, new LoginWindow());
        }
    }
}
