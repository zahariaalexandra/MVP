using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_tema3_OnlineRestaurant
{
    class Utils
    {
        static SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
   
        public static bool AddUser(User user)
        {
            int result = 0;

            connection.Open();
            SqlCommand checkCommand = new SqlCommand("CheckUserExistence", connection);
            checkCommand.CommandType = CommandType.StoredProcedure;
            checkCommand.Parameters.AddWithValue("@email", user.Email);
            checkCommand.Parameters.AddWithValue("@status", user.Status.ToString());
            SqlDataReader reader = checkCommand.ExecuteReader();

            if(reader.Read())
            {
               result = Convert.ToInt32(reader[0]);
            }

            connection.Close();

            if (result == 0)
            {
                connection.Open();
                SqlCommand insertCommand =
                    new SqlCommand("InsertUser", connection);

                insertCommand.CommandType = CommandType.StoredProcedure;
                insertCommand.Parameters.AddWithValue("@first_name", user.FirstName);
                insertCommand.Parameters.AddWithValue("@last_name", user.LastName);
                insertCommand.Parameters.AddWithValue("@email", user.Email);
                insertCommand.Parameters.AddWithValue("@password", user.Password);
                insertCommand.Parameters.AddWithValue("@telephone", user.Telephone);
                insertCommand.Parameters.AddWithValue("@address", user.Address);
                insertCommand.Parameters.AddWithValue("@status", user.Status.ToString());
                insertCommand.ExecuteNonQuery();
                connection.Close();

                return false;
            }
            else
                return true;
        }
    }

    enum Status
    {
        Employee,
        Customer
    }
}
