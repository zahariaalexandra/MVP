using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            /*connection.Open();
            SqlCommand command = new SqlCommand("procAddProduct", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", textBox.Text);
            command.Parameters.AddWithValue("@price", Convert.ToDecimal(textBox1.Text));
            command.Parameters.AddWithValue("@category", textBox2.Text);
            command.Parameters.AddWithValue("@quantity", Convert.ToDecimal(textBox3.Text));
            command.Parameters.AddWithValue("@total_quantity", Convert.ToInt32(textBox4.Text));
            command.Parameters.AddWithValue("@photo", image);
            command.ExecuteNonQuery();
            connection.Close();*/
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            string fileName = "";
            Utils.SelectImage(ref image, ref fileName);
            lblFileName.Content = fileName;
        }

        private void listCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
