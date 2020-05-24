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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        byte[] image;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                image = new byte[fileStream.Length];
                fileStream.Read(image, 0, System.Convert.ToInt32(fileStream.Length));
                fileStream.Close();
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            connection.Open();
            SqlCommand command = new SqlCommand("procAddProduct", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", textBox.Text);
            command.Parameters.AddWithValue("@price", Convert.ToDecimal(textBox1.Text));
            command.Parameters.AddWithValue("@category", textBox2.Text);
            command.Parameters.AddWithValue("@quantity", Convert.ToDecimal(textBox3.Text));
            command.Parameters.AddWithValue("@total_quantity", Convert.ToInt32(textBox4.Text));
            command.Parameters.AddWithValue("@photo", image);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
