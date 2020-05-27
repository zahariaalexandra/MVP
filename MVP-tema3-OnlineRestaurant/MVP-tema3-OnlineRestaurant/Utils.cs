using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public static int GetUser(string email, string password, string status)
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
            }

            connection.Close();

            return id;
        }

        public static List<Product> GetProductsByCategory(string category)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("procGetProductByCategory", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@category", category);
            SqlDataReader reader = command.ExecuteReader();
            List<Product> products = new List<Product>();

            while(reader.Read())
            {
                Product product = new Product();

                product.Id = Convert.ToInt32(reader[0]);
                product.Name = reader[1].ToString();
                product.Price = Convert.ToDecimal(reader[2]);
                product.Category = category;
                product.Quantity = Convert.ToUInt32(reader[4]);
                product.TotalQuantity = Convert.ToUInt32(reader[5]);

                byte[] image = (byte[])reader[6];
                MemoryStream stream = new MemoryStream();
                stream.Write(image, 0, image.Length);
                stream.Position = 0;
                BitmapImage photo = new BitmapImage();
                photo.BeginInit();
                photo.StreamSource = stream;
                photo.EndInit();
                product.Photo = photo;

                products.Add(product);
            }

            connection.Close();

            return products;
        }       

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            connection.Open();
            SqlCommand command = new SqlCommand("procGetAllProducts", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                byte[] image = (byte[])reader[6];
                MemoryStream stream = new MemoryStream();
                stream.Write(image, 0, image.Length);
                stream.Position = 0;
                BitmapImage photo = new BitmapImage();
                photo.BeginInit();
                photo.StreamSource = stream;
                photo.EndInit();

                Product product = new Product()
                {
                    Id = Convert.ToInt32(reader[0]),
                    Name = reader[1].ToString(),
                    Price = Convert.ToDecimal(reader[2]),
                    Category = reader[3].ToString(),
                    Quantity = Convert.ToUInt32(reader[4]),
                    TotalQuantity = Convert.ToUInt32(reader[5]),
                    Photo = photo
                };

                products.Add(product);
            }

            connection.Close();

            return products;
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
        LOGIN,
        MENU
    }
}
