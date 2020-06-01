using System.Windows;

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
            MenuWindow window = new MenuWindow(null, Status.NO_ACCOUNT);
            window.Show();
            Close();
        }
    }
}
