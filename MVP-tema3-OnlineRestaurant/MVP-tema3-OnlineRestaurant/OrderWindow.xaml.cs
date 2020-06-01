using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;

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
                products.Remove(products.First(s => s.Id == product.Id));

                listProducts.ItemsSource = products;
                listProducts.Items.Refresh();

                Order order = Utils.GenerateOrder(products, user);

                lblPrice.Content = order.Price.ToString();
                lblShippingValue.Content = order.Shipping.ToString();
                lblDiscountValue.Content = order.Discount.ToString();
                lblTotalValue.Content = order.FinalPrice.ToString();
            }
        }

        private void btnPlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if(products.Count == 0 || txtAddress.Text == "")
            {
                MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageIncomplete"],
                    "",
                    MessageBoxButton.OK);
                return;
            }

            MessageBoxResult result =
                MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageConfimation"],
                    "",
                    MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Order order = new Order();
                order = Utils.GenerateOrder(products, user);
                order.Address = txtAddress.Text;
                Utils.PlaceOrder(order);
                //order.Id = Utils.GetOrderId(order.StartDate, order.User);

                MessageBox.Show(ConfigurationManager.AppSettings["btnOrderCorrect"],
                    "",
                    MessageBoxButton.OK);

                products.Clear();
                Close();
            }
            else
                return;
        }
    }
}
