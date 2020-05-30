using MVP_tema3_OnlineRestaurant.Models;
using MVP_tema3_OnlineRestaurant;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

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
            listCommandTypes.ItemsSource = commandTypes;
            listFoods.SelectedIndex = -1;
            listCommandTypes.SelectedIndex = 0;
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
    }
}
