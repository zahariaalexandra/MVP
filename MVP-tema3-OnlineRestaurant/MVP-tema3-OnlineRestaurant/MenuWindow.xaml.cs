using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MVP_tema3_OnlineRestaurant
{
    public partial class MenuWindow : Window
    {
        User user;
        List<Product> chosenProducts = new List<Product>();
        public MenuWindow()
        {
            InitializeComponent();
        }

        public MenuWindow(User user, Status status)
        {
            InitializeComponent();

            List<string> categories = new List<string>();
            categories.Add("Appetizers");
            categories.Add("Salads");
            categories.Add("Soups");
            categories.Add("Rice");
            categories.Add("Noodles");
            categories.Add("Deserts");
            categories.Add("Sauces");
            categories.Add("Drinks");

            listCategories.ItemsSource = categories;
            listCategories.SelectedIndex = 0;
            listFoods.SelectedIndex = -1;

            if (user.Id != 0)
            {
                this.user = user;
                btnCreateAccount.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAdd.Visibility = Visibility.Hidden;
                btnCart.Visibility = Visibility.Hidden;
                btnProfile.Visibility = Visibility.Hidden;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show(ConfigurationManager.AppSettings["btnCancelMessage"],
                    "",
                    MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                LoginWindow window = new LoginWindow();
                window.Show();
                Close();
            }            
        }
        private void listCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string category = listCategories.SelectedItem.ToString();

            List<Product> products =
                Utils.GetProductsByCategory(category);
            listFoods.ItemsSource = products;
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            NewAccountWindow window = new NewAccountWindow(PreviousWindow.MENU, null);
            window.Show();
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(listFoods.SelectedIndex != -1)
            {
                Product product = (Product)listFoods.SelectedItem;
                product = Utils.GetProductById(product.Id);
                int totalQuantity = Convert.ToInt32(product.TotalQuantity);
                totalQuantity--;
                product.TotalQuantity = Convert.ToUInt32(totalQuantity);
                chosenProducts.Add(product);
                Utils.UpdateQuantity(product.Id, totalQuantity);
                listFoods.ItemsSource =
                    Utils.GetProductsByCategory(listCategories.SelectedItem.ToString());
            }
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow window = new OrderWindow(chosenProducts, user);
            window.ShowDialog();
            listFoods.ItemsSource =
                    Utils.GetProductsByCategory(listCategories.SelectedItem.ToString());
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            UserProfileWindow window = new UserProfileWindow();
            window.Show();
        }
    }
}
