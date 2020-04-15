using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVP_tema2_Hangman
{
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtName.Foreground = new SolidColorBrush(Colors.Black);
            txtName.FontStyle = FontStyles.Normal;
        }

        private void txtName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtName.Text = "";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtName.Text == "Type your name...")
                MessageBox.Show("Please insert your name!", "Error!", MessageBoxButton.OK);
            else
            {
                SqlConnection connection =
                    new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=Hangman-Database;Integrated Security=True;Pooling=False;Connect Timeout=30");
                SqlCommand command = new SqlCommand("InsertProcedure", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@name", SqlDbType.VarChar).Value = txtName.Text;
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Your name was added", "", MessageBoxButton.OK);
                this.Close();
                connection.Close();
            }
        }
    }
}
