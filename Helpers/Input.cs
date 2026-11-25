using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppliedProgramming
{
    public enum InputMode
    {
        None,
        Letters,   // строковое поле: цифры запрещены
        Digits,    // числовое поле: только цифры
        Phone,     // телефон: только цифры и знак «-»
        Decimal    // дробное число: цифры и один разделитель
    }

    // Присоединённое свойство Input.Mode для проверки ввода с клавиатуры (ПР №7)
    public static class Input
    {
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.RegisterAttached("Mode", typeof(InputMode), typeof(Input),
                new PropertyMetadata(InputMode.None, OnModeChanged));

        public static InputMode GetMode(DependencyObject d)
        {
            return (InputMode)d.GetValue(ModeProperty);
        }

        public static void SetMode(DependencyObject d, InputMode value)
        {
            d.SetValue(ModeProperty, value);
        }

        private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = d as TextBox;
            if (tb == null) return;

            tb.PreviewTextInput -= TextBox_PreviewTextInput;
            tb.PreviewTextInput += TextBox_PreviewTextInput;
            DataObject.RemovePastingHandler(tb, TextBox_Pasting);
            DataObject.AddPastingHandler(tb, TextBox_Pasting);
        }

        private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            e.Handled = !IsAllowed(GetMode(tb), e.Text, tb);
        }

        private static void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string text = e.DataObject.GetData(DataFormats.UnicodeText) as string;
            if (text == null || !IsAllowed(GetMode(tb), text, tb))
            {
                e.CancelCommand();
            }
        }

        private static bool IsAllowed(InputMode mode, string text, TextBox tb)
        {
            switch (mode)
            {
                case InputMode.Letters:
                    return !text.Any(char.IsDigit);

                case InputMode.Digits:
                    return text.All(char.IsDigit);

                case InputMode.Phone:
                    return text.All(c => char.IsDigit(c) || c == '-');

                case InputMode.Decimal:
                    string current = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength);
                    bool hasSeparator = current.Contains(",") || current.Contains(".");
                    foreach (char c in text)
                    {
                        if (char.IsDigit(c)) continue;
                        if ((c == ',' || c == '.') && !hasSeparator)
                        {
                            hasSeparator = true;
                            continue;
                        }
                        return false;
                    }
                    return true;

                default:
                    return true;
            }
        }

        // Разбор дробного числа с запятой или точкой
        public static bool TryParseDouble(string s, out double value)
        {
            s = (s ?? "").Trim().Replace('.', ',');
            return double.TryParse(s, NumberStyles.Float, new CultureInfo("ru-RU"), out value);
        }

        public static bool IsValidPhone(string s)
        {
            return (s ?? "").Count(char.IsDigit) >= 5;
        }
    }
}
