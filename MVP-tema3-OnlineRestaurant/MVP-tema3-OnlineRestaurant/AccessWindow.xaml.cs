using MVP_tema3_OnlineRestaurant.Models;
using System.Configuration;
using System.Windows;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class AccessWindow : Window
    {
        private Status status;

        public AccessWindow()
        {
            InitializeComponent();
        }

        public AccessWindow(Status status)
        {
            InitializeComponent();

            this.status = status;
            lblStatus.Content = lblStatus.Content + " " + status.ToString();

            if (status == Status.EMPLOYEE)
            {
                btnCreateAccount.Visibility = Visibility.Hidden;
                btnCancel.Margin = new Thickness(50, 80, 50, 10);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(txtEmail.Text == "" || txtPassword.Password == "")
            {
                MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageIncomplete"],
                    "",
                    MessageBoxButton.OK);
                return;
            }
            else
            {
                User user = 
                    Utils.GetUser(txtEmail.Text, txtPassword.Password, status);

                if(user.Id != 0)
                {
                    switch(status)
                    {
                        case Status.CUSTOMER:
                            MenuWindow menuWindow = new MenuWindow(user, status);
                            menuWindow.Show();
                            Close();
                            break;

                        case Status.EMPLOYEE:
                            AdministrationWindow administrationWindow = new AdministrationWindow(user);
                            administrationWindow.Show();
                            Close();
                            break;

                        default:
                            break;
                    }
                    
                }
                else
                {
                    MessageBox.Show(ConfigurationManager.AppSettings["btnLoginIncorrect"],
                        "",
                        MessageBoxButton.OK);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            Close();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            NewAccountWindow window = new NewAccountWindow(PreviousWindow.ACCESS, status);
            window.Show();
            Close();
        }
    }
}
