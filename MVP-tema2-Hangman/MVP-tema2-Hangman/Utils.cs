using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

            while(reader.Read())
            {
                names.Add(reader["name"].ToString());
            }
            connection.Close();

            listBoxNames.ItemsSource = names;
        }
    }
}
