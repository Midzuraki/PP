using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AppliedProgramming
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // ПР №9: один из стилей создан средствами C#
            Style label = new Style(typeof(Label));
            label.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.SemiBold));
            label.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(0, 6, 0, 2)));
            label.Setters.Add(new Setter(Control.ForegroundProperty, new DynamicResourceExtension("TextBrush")));
            Resources["FormLabel"] = label;

            // ПР №7: перемещение по Enter и сочетание Shift + 1 для всех окон
            EventManager.RegisterClassHandler(typeof(Window), Keyboard.PreviewKeyDownEvent,
                new KeyEventHandler(Window_PreviewKeyDown));

            base.OnStartup(e);
        }

        private static void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Shift + 1 -> обработчик кнопки «Назад»
            if (e.Key == Key.D1 && Keyboard.Modifiers == ModifierKeys.Shift && sender is IBackWindow back)
            {
                e.Handled = true;
                back.GoBack();
                return;
            }

            // Enter работает как Tab для полей ввода
            if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.None)
            {
                object src = e.OriginalSource;
                bool move = (src is TextBox tb && !tb.AcceptsReturn)
                         || src is PasswordBox
                         || src is CheckBox
                         || (src is ComboBox cb && !cb.IsDropDownOpen);

                if (move)
                {
                    ((UIElement)src).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    e.Handled = true;
                }
            }
        }
    }
}
