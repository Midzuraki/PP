using System.Linq;
using System.Windows;

namespace AppliedProgramming
{
    public partial class ReportWindow : Window, IBackWindow
    {
        private readonly string login;
        private readonly string password;

        public ReportWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            userBar.SetUser(login, password);

            tbInShelter.Text = Db.Pets.Count(p => !p.IsAdopted).ToString();
            tbAdopted.Text = Db.Pets.Count(p => p.IsAdopted).ToString();

            dgKinds.ItemsSource = Db.Pets.Where(p => !p.IsAdopted)
                .GroupBy(p => p.Kind)
                .Select(g => new { Kind = g.Key, Count = g.Count() })
                .OrderBy(x => x.Kind)
                .ToList();
            dgAdopt.ItemsSource = Db.Adoptions;
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
