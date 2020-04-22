﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public static void addNewPlayer(TextBox txtName, byte[] image)
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
                command.Parameters.AddWithValue("@image", image);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Name added!", "", MessageBoxButton.OK);
            }
        }

        public static void chooseImage(ref Button btnAdd, ref byte[] img, ref Label lblImage)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                FileStream fileStream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                img = new byte[fileStream.Length];
                fileStream.Read(img, 0, System.Convert.ToInt32(fileStream.Length));
                fileStream.Close();
                string fileName = dialog.FileName;
                btnAdd.IsEnabled = true;
                lblImage.Content = fileName;
            }
        }

        public static void changeImage(ListBox listBoxPlayers, ref System.Windows.Controls.Image profileImage)
        {
            ListBoxItem item = (listBoxPlayers.SelectedItem as ListBoxItem);
            SqlConnection connection =
                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();
            DataSet data = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("GetImageProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", listBoxPlayers.SelectedItem.ToString());
            adapter.SelectCommand = command;
            adapter.Fill(data);
            connection.Close();

            byte[] image = (byte[])data.Tables[0].Rows[0][0];
            MemoryStream stream = new MemoryStream();
            stream.Write(image, 0, image.Length);
            stream.Position = 0;

            var source = new BitmapImage();
            source.BeginInit();
            source.StreamSource = stream;
            source.EndInit();

            profileImage.Source = source;
        }   
    
        public static void deleteUser(ref ListBox listBoxPlayers)
        {
            SqlConnection connection =
                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();
            SqlCommand command = new SqlCommand("DeleteProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", listBoxPlayers.SelectedItem);
            command.ExecuteNonQuery();
            MessageBox.Show("User deleted!", "", MessageBoxButton.OK);
            connection.Close();

            listBoxPlayers.SelectedItem = "Ale";
        }

        public static void initializeGame(ref Game game)
        {
            game.progress = 0;
            game.level = 0;
            SqlConnection connection =
                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand command;
            Random random = new Random();
            int randomCategory = random.Next(1, 6);
            int randomWord;

            switch(randomCategory)
            {
                case 1:
                    game.category = "All categories";
                    randomWord = random.Next(1, 42);
                    connection.Open();
                    command = new SqlCommand("GetCarProcedure", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", randomWord);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        game.word = reader["name"].ToString();
                    }
                    break;
                
                case 2:
                    game.category = "All categories";
                    randomWord = random.Next(1, 19);
                    connection.Open();
                    command = new SqlCommand("GetMountainProcedure", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", randomWord);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        game.word = reader["name"].ToString();
                    }
                    break;
                
                case 3:
                    game.category = "All categories";
                    randomWord = random.Next(1, 40);
                    connection.Open();
                    command = new SqlCommand("GetMovieProcedure", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", randomWord);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        game.word = reader["name"].ToString();
                    }
                    break;
                
                case 4:
                    game.category = "All categories";
                    randomWord = random.Next(1, 18);
                    connection.Open();
                    command = new SqlCommand("GetRiverProcedure", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", randomWord);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        game.word = reader["name"].ToString();
                    }
                    break;
                
                case 5:
                    game.category = "All categories";
                    randomWord = random.Next(1, 47);
                    connection.Open();
                    command = new SqlCommand("GetStateProcedure", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", randomWord);
                    reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                         game.word = reader["name"].ToString();
                    }
                    break;
                
                default:
                    break;
            }
        }

        public static void addGame(Game game)
        {
            SqlConnection connection = 
                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();
            SqlCommand command = new SqlCommand("InsertGameProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@player_name", game.player.name);
            command.Parameters.AddWithValue("@level", game.level);
            command.Parameters.AddWithValue("@category", game.category);
            command.Parameters.AddWithValue("@word", game.word);            
            command.Parameters.AddWithValue("@used_letters", game.usedLetters.ToString());
            command.Parameters.AddWithValue("@progress", 0);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void getGame(ref Game game)
        {
            SqlConnection connection = 
                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();
            SqlCommand command = new SqlCommand("GetGameProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            game.player.name = dataSet.Tables[0].Rows[0][0].ToString();           
            game.level = Convert.ToInt32(dataSet.Tables[0].Rows[0][1].ToString());
            game.category = dataSet.Tables[0].Rows[0][2].ToString();
            game.word = dataSet.Tables[0].Rows[0][3].ToString();
            connection.Close();
        }

        public static string transformWord(string word)
        {
            StringBuilder codedWord = new StringBuilder("");

            foreach (char letter in word)
            {
                if (Char.IsLetter(letter))
                    codedWord.Append("_ ");
                else
                    codedWord.Append(letter + " ");
            }
            
             return codedWord.ToString();
        }

        public static bool letterTest(Button btn, ref Game game, ref TextBox txt, ref System.Windows.Controls.Image img, ref bool gameWon)
        {
            bool exists = false;
            StringBuilder codedWord = new StringBuilder(txt.Text.ToString());

            for (int index = 0; index < game.word.Length; index++) 
            {
                if (game.word[index].ToString() == btn.Content.ToString())
                {
                    codedWord[index * 2] = game.word[index];
                    exists = true;
                }
            }

            if(exists)
            {
                txt.Text = codedWord.ToString();
                btn.Background = new SolidColorBrush(Colors.LightGreen);
                bool found = false;

                foreach(char letter in txt.Text)
                {
                    if (letter == Convert.ToChar("_"))
                        found = true;
                }

                if(!found)
                {
                    gameWon = true;
                    return false;
                }
            }
            else
            {
                btn.Background = new SolidColorBrush(Colors.PaleVioletRed);
                   
                switch(game.progress)
                {
                    case 0:
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgHeadProgress.png"));
                        game.progress++;
                        break;
                    case 1:
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgBodyProgress.png"));
                        game.progress++;
                        break;
                    case 2:
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgOneHandProgress.png"));
                        game.progress++;
                        break;
                    case 3:
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgBothHandsProgress.png"));
                        game.progress++;
                        break;
                    case 4:
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgOneLegProgress.png"));
                        game.progress++;
                        break;
                    case 5:
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgBothLegsProgress.png"));
                        game.progress++;
                        break;
                    case 6:
                        img.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameLostProgress.png"));
                        break;
                    default:
                        break;
                }
            }

            game.addUsedLetter(btn.Content.ToString());
            btn.IsEnabled = false;

            if (game.progress == 6)
                return true;
            return false;
        }

        publi

        //private static byte[] imageToByteArray(System.Windows.Controls.Image image)
        //{
        //    BitmapImage bitmap = (image.Source as BitmapImage);
        //    int height = bitmap.PixelHeight;
        //    int width = bitmap.PixelWidth;
        //    int stride = width * ((bitmap.Format.BitsPerPixel + 7) / 8);
        //    byte[] data = new byte[height * stride];
        //    bitmap.CopyPixels(data, stride, 0);
        //    return data;
        //}

    }
}
