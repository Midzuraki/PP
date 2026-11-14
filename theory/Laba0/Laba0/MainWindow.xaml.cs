using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Laba0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<CatalogCard> CatalogCards = new ObservableCollection<CatalogCard>();
        public ObservableCollection<CartCard> CartCards = new ObservableCollection<CartCard>();
        public void UpdateCatalogCards(CatalogCard[] catalog)
        {
            this.CatalogCards = new ObservableCollection<CatalogCard>(catalog);
            catalogList.ItemsSource = CatalogCards;
        }
        public void PushCatalogCard(CatalogCard card)
        {
            this.CatalogCards.Add(card);
        }
        public void RemoveCatalogCard(int id)
        {
            foreach (CatalogCard card in this.CatalogCards)
            {
                if (card.id == id)
                {
                    this.CatalogCards.Remove(card);
                }
            }
        }
        public void ClearCatalogList()
        {
            this.CatalogCards.Clear();
        }
        public MainWindow(App app)
        {
            
            this.DataContext = this;
            InitializeComponent();
            CatalogCards = new ObservableCollection<CatalogCard>
            {
                new CatalogCard(1, "10", "20", 30, 40, 50, 60, "/food.png"),
                new CatalogCard(1, "10", "20", 30, 40, 50, 60, "/food.png"),
                new CatalogCard(1, "10", "20", 30, 40, 50, 60, "/food.png"),
                new CatalogCard(1, "10", "20", 30, 40, 50, 60, "/food.png"),
                new CatalogCard(1, "10", "20", 30, 40, 50, 60, "/food.png"),
                new CatalogCard(1, "10", "20", 30, 40, 50, 60, "/food.png"),
                new CatalogCard(1, "10", "20", 30, 40, 50, 60, "/food.png")
            };
            CartCards = new ObservableCollection<CartCard>
            {
                new CartCard(1, "10", 20, 30, 40, 50, "/food.png"),
                new CartCard(1, "10", 20, 30, 40, 50, "/food.png"),
                new CartCard(1, "10", 20, 30, 40, 50, "/food.png"),
                new CartCard(1, "10", 20, 30, 40, 50, "/food.png"),
            };
            buyout.ItemsSource = CartCards;
            catalogList.ItemsSource = CatalogCards;
        }
        public void RecieveCatalogCard(ObservableCollection<CatalogCard> cards)
        {
            this.CatalogCards = cards;
        }
    }
}
