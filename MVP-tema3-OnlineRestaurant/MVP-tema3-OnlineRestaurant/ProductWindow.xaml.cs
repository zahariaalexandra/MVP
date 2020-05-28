using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class ProductWindow : Window
    {
        Product product = new Product();

        public ProductWindow()
        {
            InitializeComponent();
        }

        public ProductWindow(string name)
        {
            InitializeComponent();

            product = Utils.GetProductByName(name);

            imgProduct.Source = product.Photo;
            txtName.Text = product.Name;
            txtInfo.Text = product.Info;
            txtQuantity.Text = product.TotalQuantity.ToString();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            int quantity = Convert.ToInt32(txtQuantity.Text);
            quantity++;
            txtQuantity.Text = quantity.ToString();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string quantity = txtQuantity.Text;
            if(Utils.OnlyDigits(quantity) && Convert.ToUInt32(quantity) >= product.TotalQuantity)
            {
                Utils.UpdateQuantity(product.Id, Convert.ToInt32(quantity));
                product = Utils.GetProductByName(product.Name);
                MessageBox.Show(ConfigurationManager.AppSettings["btnOrderCorrect"],
                    "",
                    MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(ConfigurationManager.AppSettings["btnOrderIncorrect"],
                    "",
                    MessageBoxButton.OK);
                txtQuantity.Text = product.TotalQuantity.ToString();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToUInt32(txtQuantity.Text) != product.TotalQuantity)
            {
                MessageBoxResult result =
                    MessageBox.Show(ConfigurationManager.AppSettings["btnCancelMessage"],
                        "",
                        MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                    Close();
            }
            else
                Close();
        }
    }
}
