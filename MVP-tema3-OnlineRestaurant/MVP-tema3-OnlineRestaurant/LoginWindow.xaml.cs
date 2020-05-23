using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            NewAccountWindow window = new NewAccountWindow(PreviousWindow.LOGIN, null);
            window.Show();
            this.Close();
        }

        private void btnCustomerAccount_Click(object sender, RoutedEventArgs e)
        {
            AccessWindow window = new AccessWindow(Status.CUSTOMER);
            window.Show();
            Close();
        }

        private void btnEmployeeAccount_Click(object sender, RoutedEventArgs e)
        {
            AccessWindow window = new AccessWindow(Status.EMPLOYEE);
            window.Show();
            Close();
        }

        private void btnNoAccount_Click(object sender, RoutedEventArgs e)
        {
            MenuWindow window = new MenuWindow(null, null, Status.NO_ACCOUNT);
        }
    }
}
