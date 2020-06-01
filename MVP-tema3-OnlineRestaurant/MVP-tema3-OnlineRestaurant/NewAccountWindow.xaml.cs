using MVP_tema3_OnlineRestaurant.Models;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            if(window == PreviousWindow.ACCESS)
            {
                AccessWindow accessWindow = new AccessWindow(status);
                accessWindow.Show();
                Close();
            }
            else if(window == PreviousWindow.LOGIN || window == PreviousWindow.MENU)
            {
                MessageBoxResult result =
                         MessageBox.Show(ConfigurationManager.AppSettings["btnCancelMessage"],
                         "",
                         MessageBoxButton.YesNo);

                if(result == MessageBoxResult.Yes && window == PreviousWindow.LOGIN)
                {
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.Show();
                    this.Close();
                }
                else if(result == MessageBoxResult.Yes && window == PreviousWindow.MENU)
                {
                    MenuWindow menuWindow = new MenuWindow(null, Status.NO_ACCOUNT);
                    menuWindow.Show();
                    Close();
                }
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

            if(txtPassword.Password == "")
            {
                MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageIncomplete"],
                            "",
                            MessageBoxButton.OK);
                return;
            }

            user.FirstName = txtFirstName.Text;
            user.LastName = txtLastName.Text;
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Password;
            user.Telephone = txtTelephone.Text;
            user.Address = txtAddress.Text;

            user.Status = Status.CUSTOMER;

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
