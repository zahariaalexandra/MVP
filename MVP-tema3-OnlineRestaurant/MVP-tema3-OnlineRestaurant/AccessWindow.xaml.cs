using System;
using System.Collections.Generic;
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
