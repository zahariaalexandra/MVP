using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MVP_tema3_OnlineRestaurant
{
    public partial class ProductWindow : Window
    {
        Product product = new Product();

        public ProductWindow()
        {
            InitializeComponent();
        }

        public ProductWindow(int id)
        {
            InitializeComponent();

            product = Utils.GetProductById(id);

            MemoryStream stream = new MemoryStream();
            stream.Write(product.Photo, 0, product.Photo.Length);
            stream.Position = 0;
            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = stream;
            source.EndInit();

            imgProduct.Source = source;
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
                product = Utils.GetProductById(product.Id);
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
