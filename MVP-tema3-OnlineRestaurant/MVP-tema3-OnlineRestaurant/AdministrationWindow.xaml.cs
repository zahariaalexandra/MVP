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

namespace MVP_tema3_OnlineRestaurant
{
    public partial class AdministrationWindow : Window
    {
        int id;

        public AdministrationWindow()
        {
            InitializeComponent();
        }

        public AdministrationWindow(int id)
        {
            InitializeComponent();

            this.id = id;
            List<Product> products = Utils.GetAllProducts();
            List<string> commandTypes = new List<string>();
            commandTypes.Add("All commands");
            commandTypes.Add("Active commands");
            listFoods.ItemsSource = products;
            listCommandTypes.ItemsSource = commandTypes;
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            //int quantity = listFoods.SelectedItem.txt

            /*var parent = (sender as Button).Parent;
            TextBox txtQuantity = parent.GetChildrenOfType<TextBox>().First(x => x.Name == "txtQuantityTotal");
            int quantity = Convert.ToInt32*/
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
