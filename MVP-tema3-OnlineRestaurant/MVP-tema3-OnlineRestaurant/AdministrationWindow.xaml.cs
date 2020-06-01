using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class AdministrationWindow : Window
    {
        User user;

        public AdministrationWindow()
        {
            InitializeComponent();
        }

        public AdministrationWindow(User user)
        {
            InitializeComponent();

            this.user = user;
            List<Product> products = Utils.GetAllProducts();
            List<string> commandTypes = new List<string>();
            commandTypes.Add("All commands");
            commandTypes.Add("Active commands");
            listFoods.ItemsSource = products;
            listOrderTypes.ItemsSource = commandTypes;
            listFoods.SelectedIndex = -1;
            listOrderTypes.SelectedIndex = 0;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            if(listFoods.SelectedIndex != -1)
            {
                Product selectedItem = new Product();
                selectedItem = (Product)listFoods.SelectedItem;
                int id = selectedItem.Id;
                ProductWindow window = new ProductWindow(id);
                window.ShowDialog();
                listFoods.ItemsSource = Utils.GetAllProducts();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(listFoods.SelectedIndex != -1)
            {
                Product selectedItem = new Product();
                selectedItem = (Product)listFoods.SelectedItem;
                int id = selectedItem.Id;
                Product product = Utils.GetProductById(id);
                Utils.DeleteProduct(product);
                listFoods.SelectedIndex = -1;
                listFoods.ItemsSource = Utils.GetAllProducts();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow window = new AddProductWindow();
            window.ShowDialog();
            listFoods.ItemsSource = Utils.GetAllProducts();
        }

        private void listOrderTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listOrderTypes.SelectedIndex != -1)
            {
                listOrders.ItemsSource = Utils.GetAllOrders(listOrderTypes.SelectedItem.ToString());
                listOrders.SelectedIndex = -1;
            }
        }

        private void btnCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (listOrders.SelectedIndex != -1)
            {
                Order order = (Order)listOrders.SelectedItem;

                if (order.Status == OrderProgress.IN_PROGRESS)
                {
                    MessageBoxResult result =
                        MessageBox.Show(ConfigurationManager.AppSettings["btnCancelOrder"],
                            "",
                            MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        Utils.UpdateOrderStatus(order, OrderProgress.CANCELED);
                        listOrders.ItemsSource = Utils.GetOrdersByUser(user);
                        Utils.RestockProducts(order.Products);
                    }
                }
            }
        }

        private void btnDeliverOrder_Click(object sender, RoutedEventArgs e)
        {
            if (listOrders.SelectedIndex != -1)
            {
                Order order = (Order)listOrders.SelectedItem;

                if (order.Status == OrderProgress.IN_PROGRESS)
                {
                    MessageBox.Show(ConfigurationManager.AppSettings["btnOrderCorrect"],
                        "",
                        MessageBoxButton.OK);
                    Utils.UpdateOrderStatus(order, OrderProgress.DELIVERED);
                    order.FinishDate = DateTime.Now;
                    listOrders.ItemsSource = Utils.GetOrdersByUser(user);
                }
            }
        }
    }
}
