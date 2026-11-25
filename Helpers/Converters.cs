using System;
using System.Globalization;
using System.Windows.Data;

namespace AppliedProgramming
{
    // Для мультитриггера полей ввода (ПР №10):
    // NotEmpty  - строка не пустая
    // LessThan5 - в строке меньше 5 символов
    public class TextLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string ?? "";
            switch (parameter as string)
            {
                case "NotEmpty": return s.Length > 0;
                case "LessThan5": return s.Length < 5;
                default: return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
