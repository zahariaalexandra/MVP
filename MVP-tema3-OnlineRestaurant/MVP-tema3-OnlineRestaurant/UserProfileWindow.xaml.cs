using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Configuration;

namespace MVP_tema3_OnlineRestaurant
{
    /// <summary>
    /// Interaction logic for UserProfileWindow.xaml
    /// </summary>
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
                        
                        foreach(Product product in order.Products)
                        {
                            Product tempProduct = Utils.GetProductById(product.Id);
                            int quantity = Convert.ToInt32(tempProduct.TotalQuantity);
                            quantity++;
                            Utils.UpdateQuantity(product.Id, quantity);
                        }
                    }                    
                }
            }
        }
    }
}
