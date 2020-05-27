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
        int id;

        public MenuWindow()
        {
            InitializeComponent();
        }

        public MenuWindow(int? id, Status status)
        {
            InitializeComponent();
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes

            List<string> categories = new List<string>();
            categories.Add("Appetizers");
            categories.Add("Salads");
            categories.Add("Soups");
            categories.Add("Rice");
            categories.Add("Noodles");
            categories.Add("Deserts");
            categories.Add("Sauces");
            categories.Add("Drinks");
<<<<<<< Updated upstream
            categories.Add("Menus");
            listCategories.ItemsSource = categories;

=======
            listCategories.ItemsSource = categories;

>>>>>>> Stashed changes
>>>>>>> Stashed changes
            listCategories.SelectedIndex = 0;

            if(id != null)
            {
                this.id = Convert.ToInt32(id);
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
<<<<<<< Updated upstream
=======
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes

        private void listCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string category = listCategories.SelectedItem.ToString();

<<<<<<< Updated upstream
            if(category != "Menus")
            {
                List<Product> products =
                    Utils.GetProductsByCategory(category);
                listFoods.ItemsSource = products;
            } 
            else
            {

            }           
        }
=======
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
>>>>>>> Stashed changes
>>>>>>> Stashed changes
    }
}
