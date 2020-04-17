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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtName.Text == "Type your name...")
                MessageBox.Show("Please insert your name!", "Error!", MessageBoxButton.OK);
            else
            {
                SqlConnection connection =
                    new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=HangmanDatabase;Integrated Security=True;Pooling=False;Connect Timeout=30");
                connection.Open();

                string query = @"insert into HangmanDatabase.Players(name) values(@name)";
                SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@name",SqlDbType.VarChar).Value = txtName.Text;
                command.ExecuteNonQuery();
                MessageBox.Show("Your name was added", "", MessageBoxButton.OK);
               
                connection.Close();
                this.Close();
            }
        }

        private void txtName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtName.Text = "";
        }
    }
}
