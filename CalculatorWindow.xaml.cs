using System;
using System.Windows;
using System.Windows.Controls;

namespace AppliedProgramming
{
    public partial class CalculatorWindow : Window
    {
        private string leftOp = "";
        private string rightOp = "";
        private string operation = "";

        private bool justCalculated = false;

        public CalculatorWindow()
        {
            InitializeComponent();

            foreach (UIElement c in MainGrid().Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }

        private Grid MainGrid()
        {
            return (Grid)this.Content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Content.ToString();

            if (s == "C")
            {
                Clear();
            }
            else if (s == "=")
            {
                Calculate();
            }
            else if (IsOperation(s))
            {
                SetOperation(s);
            }
            else 
            {
                AddDigit(s);
            }
        }

        private static bool IsOperation(string s)
        {
            return s == "+" || s == "−" || s == "×" || s == "÷";
        }

        private void AddDigit(string digit)
        {
            if (operation == "")
            {
                if (justCalculated)
                {
                    leftOp = "";
                    justCalculated = false;
                }
                leftOp = AppendDigit(leftOp, digit);
                tbDisplay.Text = leftOp;
            }
            else
            {
                rightOp = AppendDigit(rightOp, digit);
                tbDisplay.Text = rightOp;
            }
        }

        private static string AppendDigit(string number, string digit)
        {
            if (number.Length >= 12) return number;
            if (number == "0") return digit;
            return number + digit;
        }

        private void SetOperation(string op)
        {
            if (leftOp == "")
            {
                leftOp = "0";
            }

            if (rightOp != "")
            {
                Calculate();
                if (leftOp == "") return;
            }

            operation = op;
            justCalculated = false;
            tbDisplay.Text = leftOp + " " + op;
        }

        private void Calculate()
        {
            if (leftOp == "" || operation == "" || rightOp == "")
            {
                return;
            }

            decimal a = decimal.Parse(leftOp);
            decimal b = decimal.Parse(rightOp);
            decimal result = 0;

            try
            {
                switch (operation)
                {
                    case "+": result = a + b; break;
                    case "−": result = a - b; break;
                    case "×": result = a * b; break;
                    case "÷":
                        if (b == 0)
                        {
                            ShowError("Деление на 0");
                            return;
                        }
                        result = a / b;
                        break;
                }
            }
            catch (OverflowException)
            {
                ShowError("Переполнение");
                return;
            }

            result = Math.Round(result, 8);
            leftOp = result.ToString("0.########");
            rightOp = "";
            operation = "";
            justCalculated = true;
            tbDisplay.Text = leftOp;
        }

        private void ShowError(string message)
        {
            leftOp = "";
            rightOp = "";
            operation = "";
            justCalculated = false;
            tbDisplay.Text = message;
        }

        private void Clear()
        {
            leftOp = "";
            rightOp = "";
            operation = "";
            justCalculated = false;
            tbDisplay.Text = "0";
        }
    }
}
