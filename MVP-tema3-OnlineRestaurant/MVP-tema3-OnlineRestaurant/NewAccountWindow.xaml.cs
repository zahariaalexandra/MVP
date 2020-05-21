using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class NewAccountWindow : Window
    {
        string originalText = "";

        public NewAccountWindow()
        {
            InitializeComponent();
        }

        private void txtPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if(textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
                originalText = textBox.Text;
            }
        }

        private void txtLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text == "")
            {
                textBox.Text = originalText;
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(ConfigurationManager.AppSettings["btnCancelMessage"], "", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                LoginWindow window = new LoginWindow();
                window.Show();
                this.Close();
            }
        }

    }
}
