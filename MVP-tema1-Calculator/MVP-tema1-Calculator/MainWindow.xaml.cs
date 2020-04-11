using System;
using System.Windows;


namespace MVP_tema1_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int op1 = 0;
        private int op2 = 0;
        private string operation = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void addDigit(int digit)
        {
            if(operation=="")
            {
                op1 = (op1 * 10) + digit;
                txtInput.Text = op1.ToString();
            }
            else
            {
                op2 = (op2 * 10) + digit;
                txtInput.Text = op2.ToString();
            }
        }

        private void clickOperation(string str)
        {
            operation = str;
            txtInput.Text = str;
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn0.Content));
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn1.Content));
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn2.Content));
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn3.Content));
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn4.Content));
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn5.Content));
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn6.Content));
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn7.Content));
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn8.Content));
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            addDigit(Convert.ToInt32(btn9.Content));
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            clickOperation("+");
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            clickOperation("-");
        }

        private void btnMultiply_Click(object sender, RoutedEventArgs e)
        {
            clickOperation("*");
        }

        private void btnDivide_Click(object sender, RoutedEventArgs e)
        {
            clickOperation("/");
        }

        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            switch(operation)
            {
                case "+":
                    txtInput.Text = (op1 + op2).ToString();
                    op1 += op2;
                    op2 = 0;
                    break;
                case "-":
                    txtInput.Text = (op1 - op2).ToString();
                    op1 -= op2;
                    op2 = 0;
                    break;
                case "*":
                    txtInput.Text = (op1 * op2).ToString();
                    op1 *= op2;
                    op2 = 0;
                    break;
                case "/":
                    txtInput.Text = (op1 / op2).ToString();
                    op1 /= op2;
                    op2 = 0;
                    break;
                default:
                    break;
            }
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
                op1 = 0;
            else
                op2 = 0;

            txtInput.Text = "";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            op1 = 0;
            op2 = 0;
            operation = "";
            txtInput.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(operation=="")
            {
                op1 /= 10;
                txtInput.Text = op1.ToString();
            }
            else
            {
                op2 /= 10;
                txtInput.Text = op2.ToString();
            }
        }

        private void btnPositivity_Click(object sender, RoutedEventArgs e)
        {
            if (operation == "")
            {
                op1 *= -1;
                txtInput.Text = op1.ToString();
            }
            else
            {
                op2 *= -1;
                txtInput.Text = op2.ToString();
            }
        }
    }
}
