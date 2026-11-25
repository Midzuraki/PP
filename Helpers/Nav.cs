using System.Windows;

namespace AppliedProgramming
{
    // Окна, у которых есть кнопка «Назад» (срабатывает и по Shift + 1, ПР №7)
    public interface IBackWindow
    {
        void GoBack();
    }

    public static class Nav
    {
        // Переход: показать новое окно и закрыть текущее
        public static void Go(Window from, Window to)
        {
            to.Show();
            from.Close();
        }
    }
}
