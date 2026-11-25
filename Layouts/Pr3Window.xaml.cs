using System.Windows;

namespace AppliedProgramming
{
    public partial class Pr3Window : Window
    {
        public Pr3Window()
        {
            InitializeComponent();
        }

        private void V1_Click(object sender, RoutedEventArgs e) { new Pr3Variant1Window().Show(); }
        private void V2_Click(object sender, RoutedEventArgs e) { new Pr3Variant2Window().Show(); }
    }
}
