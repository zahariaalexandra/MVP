using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.CodeDom;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class OrderWindow : Window
    {
        List<Product> products;
        User user;

        public OrderWindow()
        {
            InitializeComponent();
        }

        public OrderWindow(List<Product> products, User user)
        {
            InitializeComponent();

            this.user = user;
            this.products = products;

            txtAddress.Text = user.Address;
            listProducts.ItemsSource = products;
            listProducts.SelectedIndex = -1;

            Order order = Utils.GenerateOrder(products, user);

            lblPrice.Content = order.Price.ToString();
            lblShippingValue.Content = order.Shipping.ToString();
            lblDiscountValue.Content = order.Discount.ToString();
            lblTotalValue.Content = order.FinalPrice.ToString();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(listProducts.SelectedIndex != -1)
            {
                Product product = (Product)listProducts.SelectedItem;
                product = Utils.GetProductById(product.Id);
                uint quantity = product.TotalQuantity;
                quantity++;
                Utils.UpdateQuantity(product.Id, Convert.ToInt32(quantity));
                products.Remove(product);
                listProducts.ItemsSource = products;

                Order order = Utils.GenerateOrder(products, user);

                lblPrice.Content = order.Price.ToString();
                lblShippingValue.Content = order.Shipping.ToString();
                lblDiscountValue.Content = order.Discount.ToString();
                lblTotalValue.Content = order.FinalPrice.ToString();
            }
        }
    }
}
