using System.Windows;
using System.Windows.Controls;

namespace AppliedProgramming
{
    public partial class InterfaceWindow : Window
    {
        public InterfaceWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            string l = "admin", p = "admin123";
            Window w = null;

            switch ((string)((Button)sender).Tag)
            {
                case "login": w = new LoginWindow(); break;
                case "register": w = new RegisterWindow(); break;
                case "menu": w = new MenuWindow(l, p); break;
                case "pets": w = new PetsWindow(l, p); break;
                case "request": w = new RequestWindow(l, p); break;
                case "addpet": w = new AddPetWindow(l, p); break;
                case "adopt": w = new AdoptWindow(l, p); break;
                case "vet": w = new VetWindow(l, p); break;
                case "report": w = new ReportWindow(l, p); break;
            }
            if (w != null) w.Show();
        }
    }
}
