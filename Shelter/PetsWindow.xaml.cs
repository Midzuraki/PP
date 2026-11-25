using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AppliedProgramming
{
    public partial class PetsWindow : Window, IBackWindow
    {
        private const string All = "Все";
        private readonly string login;
        private readonly string password;
        private ICollectionView view;

        public PetsWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
            userBar.SetUser(login, password);

            cbKind.ItemsSource = WithAll(Db.Pets.Select(p => p.Kind));
            cbBreed.ItemsSource = WithAll(Db.Pets.Select(p => p.Breed));
            cbGender.ItemsSource = WithAll(Db.Genders);

            view = CollectionViewSource.GetDefaultView(Db.Pets);
            view.Filter = Match;
            dgPets.ItemsSource = view;

            btnReset_Click(null, null);
            Closed += (s, e) => view.Filter = null;
        }

        private static List<string> WithAll(IEnumerable<string> items)
        {
            List<string> list = new List<string> { All };
            list.AddRange(items.Distinct().OrderBy(x => x));
            return list;
        }

        private bool Match(object o)
        {
            Pet p = (Pet)o;
            return Pass(cbKind, p.Kind) && Pass(cbBreed, p.Breed) && Pass(cbGender, p.Gender);
        }

        private static bool Pass(ComboBox cb, string value)
        {
            string selected = cb.SelectedItem as string;
            return selected == null || selected == All || selected == value;
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (view != null) view.Refresh();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cbKind.SelectedIndex = 0;
            cbBreed.SelectedIndex = 0;
            cbGender.SelectedIndex = 0;
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
