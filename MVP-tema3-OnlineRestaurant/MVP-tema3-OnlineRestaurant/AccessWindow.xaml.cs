using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string firstName = "";
                string lastName = "";
                int id = 
                    Utils.GetUser(txtEmail.Text, txtPassword.Password, status.ToString(), ref firstName, ref lastName);

                if(id != 0 && firstName != "" && lastName != "")
                {
                    switch(status)
                    {
                        case Status.CUSTOMER:
                            MenuWindow menuWindow = new MenuWindow(id, status);
                            menuWindow.Show();
                            Close();
                            break;

                        case Status.EMPLOYEE:
                            AdministrationWindow administrationWindow = new AdministrationWindow(id, firstName, lastName);
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
