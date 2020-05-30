using MVP_tema3_OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

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

        public static User GetUser(string email, string password, Status status)
        {
            User user = new User();

            connection.Open();
            SqlCommand command = new SqlCommand("procGetUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@status", status.ToString());
            SqlDataReader reader = command.ExecuteReader();

            if(reader.Read())
            {
                user.Id = Convert.ToInt32(reader[0]);
                user.FirstName = reader[1].ToString();
                user.LastName = reader[2].ToString();
                user.Email = reader[3].ToString();
                user.Password = reader[4].ToString();
                user.Telephone = reader[5].ToString();
                user.Address = reader[6].ToString();
                user.Status = status;
            }

            connection.Close();

            return user;
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
                product.Active = Convert.ToBoolean(reader[7]);

                byte[] image = (byte[])reader[6];
                MemoryStream stream = new MemoryStream();
                stream.Write(image, 0, image.Length);
                //stream.Position = 0;
                product.Photo = image;
                /*BitmapImage photo = new BitmapImage();
                photo.BeginInit();
                photo.StreamSource = stream;
                photo.EndInit();
                product.Photo = photo;*/

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
                /*stream.Position = 0;
                BitmapImage photo = new BitmapImage();
                photo.BeginInit();
                photo.StreamSource = stream;
                photo.EndInit();*/

                Product product = new Product()
                {
                    Id = Convert.ToInt32(reader[0]),
                    Name = reader[1].ToString(),
                    Price = Convert.ToDecimal(reader[2]),
                    Category = reader[3].ToString(),
                    Quantity = Convert.ToUInt32(reader[4]),
                    TotalQuantity = Convert.ToUInt32(reader[5]),
                    Photo = image,
                    Active = Convert.ToBoolean(reader[7])
                };

                products.Add(product);
            }

            connection.Close();

            return products;
        }

        public static Product GetProductById(int id)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("procGetProductById", connection);
            command.CommandType =    CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
            Product product = new Product();

            if(reader.Read())
            {
                product.Id = id;
                product.Name = reader[1].ToString();
                product.Price = Convert.ToDecimal(reader[2]);
                product.Category = reader[3].ToString();
                product.Quantity = Convert.ToUInt32(reader[4]);
                product.TotalQuantity = Convert.ToUInt32(reader[5]);

                byte[] image = (byte[])reader[6];
                MemoryStream stream = new MemoryStream();
                stream.Write(image, 0, image.Length);
                /*stream.Position = 0;
                BitmapImage photo = new BitmapImage();
                photo.BeginInit();
                photo.StreamSource = stream;
                photo.EndInit();*/
                product.Photo = image;
            }

            connection.Close();

            return product;
        }

        public static bool OnlyDigits(string quantity)
        {
            foreach(char ch in quantity)
            {
                if (!char.IsDigit(ch))
                    return false;
            }

            return true;
        }

        public static void UpdateQuantity(int id, int quantity)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("procUpdateQuantity", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@quantity", quantity);

            if (quantity == 0)
                command.Parameters.AddWithValue("@active", false);
            else
                command.Parameters.AddWithValue("@active", true);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void DeleteProduct(Product product)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("procDeleteProduct", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteReader();
            connection.Close();
        }

        public static void SelectImage(ref byte[] image, ref string fileName)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            //dialog.DefaultExt = ".png";
            dialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                image = new byte[fileStream.Length];
                fileStream.Read(image, 0, System.Convert.ToInt32(fileStream.Length));
                fileStream.Close();
                fileName = Path.GetFileName(dialog.FileName);
            }
        }

        public static bool IsDecimal(string text)
        {
            bool dot = false;
            int nrDot = 0;
            int digits = 0;

            foreach(char ch in text)
            {
                if ((!char.IsDigit(ch) && ch != '.') || nrDot > 1 || digits > 2)
                    return false;

                if (ch == '.')
                {
                    dot = true;
                    nrDot++;
                }

                if (char.IsDigit(ch) && dot)
                    digits++;
            }

            if (!dot || digits < 2)
                return false;

            return true;
        }

        public static void AddProduct(Product product, byte[] image)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("procAddProduct", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@category", product.Category);
            command.Parameters.AddWithValue("@quantity", Convert.ToInt32(product.Quantity));
            command.Parameters.AddWithValue("@total_quantity", Convert.ToInt32(product.TotalQuantity));
            command.Parameters.AddWithValue("@photo", image);
            command.Parameters.AddWithValue("@active", product.Active);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static Order GenerateOrder(List<Product> products, User user)
        {
            decimal price = 0;

            foreach(Product product in products)
            {
                price += product.Price;
            }

            Order order = new Order()
            {
                User = user,
                Products = products,
                StartDate = DateTime.Now,
                Price = price
            };

            return order;
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
