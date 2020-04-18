using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace MVP_tema2_Hangman
{
    class Utils
    {
        public static void getNames(ref ListBox listBoxNames)
        {
            List<string> names = new List<string>();

            SqlConnection connection =
                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();
            SqlCommand command = new SqlCommand("GetNamesProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                names.Add(reader["name"].ToString());
            }
            connection.Close();

            listBoxNames.ItemsSource = names;
        }

        public static void addNewPlayer(TextBox txtName, string imageString)
        {
            if (txtName.Text == "" || txtName.Text == "Type your name...")
                MessageBox.Show("Please insert your name!", "Error!", MessageBoxButton.OK);
            else
            {
                SqlConnection connection =
                    new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                connection.Open();
                SqlCommand command = new SqlCommand("InsertProcedure", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@name", txtName.Text);
                command.Parameters.AddWithValue("@image_string", imageString);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Name added!", "", MessageBoxButton.OK);
            }
        }
    }
}
