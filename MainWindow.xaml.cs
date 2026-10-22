using System.Windows;

namespace AppliedProgramming
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // ПР №1: открыть калькулятор
        private void btnPr1_Click(object sender, RoutedEventArgs e)
        {
            CalculatorWindow w = new CalculatorWindow();
            w.Show();
        }

        // ПР №2: открыть окно с компоновкой
        private void btnPr2_Click(object sender, RoutedEventArgs e)
        {
            CardWindow w = new CardWindow();
            w.Show();
        }
    }
}
