using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MVP_tema3_OnlineRestaurant
{
    public class Utils
    {
        static SqlConnection connection =
            new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
   
        public static bool AddUser(User user)
        {
            int result = 0;

            connection.Open();
            SqlCommand checkCommand = new SqlCommand("procCheckUserExistence", connection);
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
                    new SqlCommand("procInsertUser", connection);

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

        public static int GetUser(string email, string password, string status, ref string firstName, ref string lastName)
        {
            int id = 0;

            connection.Open();
            SqlCommand command = new SqlCommand("procGetUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@status", status);
            SqlDataReader reader = command.ExecuteReader();

            if(reader.Read())
            {
                id = Convert.ToInt32(reader[0]);
                firstName = reader[1].ToString();
                lastName = reader[2].ToString();
            }

            if (firstName != "" && lastName != "")
                return id;

            return id;
        }
    }

    public enum Status
    {
        EMPLOYEE,
        CUSTOMER,
        NO_ACCOUNT
    }

    public enum PreviousWindow
    {
        ACCESS,
        LOGIN
    }
}
