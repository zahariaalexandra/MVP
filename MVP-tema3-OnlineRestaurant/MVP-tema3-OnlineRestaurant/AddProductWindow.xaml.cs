using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class AddProductWindow : Window
    {
        byte[] image = new byte[0];

        public AddProductWindow()
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

            listCategory.ItemsSource = categories;
            listCategory.SelectedIndex = -1;
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";
            Utils.SelectImage(ref image, ref fileName);
            txtFileName.Text = fileName;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            foreach (TextBox textBox in grid.Children.OfType<TextBox>())
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageIncomplete"],
                            "",
                            MessageBoxButton.OK);
                    return;
                }
            }

            if(listCategory.SelectedIndex == -1 || txtFileName.Text == "...")
            {
                MessageBox.Show(ConfigurationManager.AppSettings["btnContinueMessageIncomplete"],
                            "",
                            MessageBoxButton.OK);
                return;
            }

            if(!Utils.OnlyDigits(txtQuantity.Text) || 
                !Utils.OnlyDigits(txtTotalQuantity.Text) || 
                !Utils.IsDecimal(txtPrice.Text))
            {
                MessageBox.Show(ConfigurationManager.AppSettings["btnOrderIncorrect"],
                           "",
                           MessageBoxButton.OK);
                return;
            }

            Product product = new Product();
            product.Name = txtName.Text;
            product.Price = Convert.ToDecimal(txtPrice.Text);
            product.Category = listCategory.SelectedItem.ToString();
            product.Quantity = Convert.ToUInt32(txtQuantity.Text);
            product.TotalQuantity = Convert.ToUInt32(txtTotalQuantity.Text);
            product.Active = (Convert.ToUInt32(txtTotalQuantity.Text) == 0);

            Utils.AddProduct(product, image);

            MessageBox.Show(ConfigurationManager.AppSettings["btnOrderCorrect"],
                "",
                MessageBoxButton.OK);

            foreach (TextBox textBox in grid.Children.OfType<TextBox>())
            {
                textBox.Text = "";
            }

            txtFileName.Text = "...";
            listCategory.SelectedIndex = -1;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show(ConfigurationManager.AppSettings["btnCancelMessage"],
                "",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                Close();
        }
    }
}
