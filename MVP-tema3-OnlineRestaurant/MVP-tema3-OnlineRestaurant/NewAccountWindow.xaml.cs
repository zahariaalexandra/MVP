using MVP_tema3_OnlineRestaurant.Models;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class NewAccountWindow : Window
    {
        private PreviousWindow window;
        private Status status;

        public NewAccountWindow()
        {
            InitializeComponent();
        }

        public NewAccountWindow(PreviousWindow window, Status? status)
        {
            InitializeComponent();

            this.window = window;
            if(status != null)
                this.status = (Status)status;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            switch(window)
            {
                case PreviousWindow.ACCESS:
                    AccessWindow accessWindow = new AccessWindow(status);
                    accessWindow.Show();
                    Close();
                    break;

                case PreviousWindow.LOGIN:
                    MessageBoxResult result =
                         MessageBox.Show(ConfigurationManager.AppSettings["btnCancelMessage"],
                         "",
                         MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        this.Close();
                    }

                    break;

                default:
                    break;
            }
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();

            foreach(TextBox textBox in grid.Children.OfType<TextBox>())
            {
                if(textBox.Text == "")
                {
                    MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageIncomplete"],
                            "",
                            MessageBoxButton.OK);
                    return;
                }
            }

            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Password;
            user.Telephone = txtTelephone.Text;
            user.Address = txtAddress.Text;

            if (checkCustomer.IsChecked == false && checkEmployee.IsChecked == false)
            {
                MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageIncomplete"],
                        "",
                        MessageBoxButton.OK);
                return;
            } 
            else if (checkCustomer.IsChecked == true)
                user.Status = Status.CUSTOMER;
            else
                user.Status = Status.EMPLOYEE;
            
            MessageBoxResult result = 
                MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageConfimation"],
                "",
                MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                if (!Utils.AddUser(user))
                {
                    MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageCreated"],
                    "",
                    MessageBoxButton.OK);
                    LoginWindow window = new LoginWindow();
                    window.Show();
                    Close();
                }
                else
                    MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageNotCreated"],
                        "",
                        MessageBoxButton.OK);
            }
        }
    }
}
