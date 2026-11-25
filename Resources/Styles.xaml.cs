using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppliedProgramming
{
    // Обработчики событий, подключённые через стили (EventSetter, ПР №9)
    public partial class StylesDictionary : ResourceDictionary
    {
        public StylesDictionary()
        {
            InitializeComponent();
        }

        // При получении фокуса выделяем всё содержимое поля
        private void ValidatedBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.SelectAll();
            }
            else if (sender is PasswordBox pb)
            {
                pb.SelectAll();
            }
        }

        // Двойной щелчок выделяет весь текст
        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
