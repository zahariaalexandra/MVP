using MVP_tema3_OnlineRestaurant.Models;
using System.Collections.Generic;
using System.Windows;
using System.Configuration;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class UserProfileWindow : Window
    {
        User user;

        public UserProfileWindow()
        {
            InitializeComponent();
        }

        public UserProfileWindow(User user)
        {
            InitializeComponent();

            this.user = user;

            txtName.Text = user.FullName;
            lblEmailValue.Content = user.Email;
            lblTelephoneValue.Content = user.Telephone;
            lblAddressValue.Text = user.Address;

            List<Order> orders = Utils.GetOrdersByUser(user);
            listOrders.ItemsSource = orders;
            listOrders.SelectedIndex = -1;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if(listOrders.SelectedIndex != -1)
            {
                Order order = (Order)listOrders.SelectedItem;
                
                if(order.Status == OrderProgress.IN_PROGRESS)
                {
                    MessageBoxResult result = 
                        MessageBox.Show(ConfigurationManager.AppSettings["btnCancelOrder"],
                            "",
                            MessageBoxButton.YesNo);

                    if(result == MessageBoxResult.Yes)
                    {
                        Utils.UpdateOrderStatus(order, OrderProgress.CANCELED);
                        listOrders.ItemsSource = Utils.GetOrdersByUser(user);
                        Utils.RestockProducts(order.Products);
                    }                    
                }
            }
        }
    }
}
