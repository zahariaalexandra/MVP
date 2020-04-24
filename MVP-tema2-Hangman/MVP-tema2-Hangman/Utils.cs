using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;

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

        public static void initializeGame(ref Game game, int category)
        {
            game.progress = 0;
            SqlConnection connection =
                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlCommand command;
            Random random = new Random();            
            int randomWord;
           
            switch(category)
            {
                case 0:
                    int randomCategory = random.Next(1, 6);
                    game.category = "All categories";
                    category = randomCategory;
                    break;
                case 1:
                    game.category = "Cars";
                    break;
                case 2:
                    game.category = "Mountains";
                    break;
                case 3:
                    game.category = "Movies & series";
                    break;
                case 4:
                    game.category = "Rivers";
                    break;
                case 5:
                    game.category = "States";
                    break;
                default:
                    break;
            }

            switch (category)
            {
                case 1:
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

        public static void addGame(Game game, bool saved)
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
            command.Parameters.AddWithValue("@progress", game.progress);
            command.Parameters.AddWithValue("@saved", saved);
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
            game.id = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
            game.player.name = dataSet.Tables[0].Rows[0][1].ToString();           
            game.level = Convert.ToInt32(dataSet.Tables[0].Rows[0][2].ToString());
            game.category = dataSet.Tables[0].Rows[0][3].ToString();
            game.word = dataSet.Tables[0].Rows[0][4].ToString();
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

        public static void updatePlayer(string playerName, bool game)
        {
            SqlConnection connection =
                new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connection.Open();
            SqlCommand command;

            if(game)
            {
                command = new SqlCommand("UpdateWonProcedure", connection);
            }
            else
            {
                command = new SqlCommand("UpdateLostprocedure", connection);
            }

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", playerName);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void fillStatisticsTable(ref Table tbl)
        {
            SqlConnection connection =
               new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            int row = 1;

            connection.Open();
            SqlCommand command = new SqlCommand("GetPlayerProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = command.ExecuteReader();
            
            while(reader.Read())
            {
                Player player = new Player();
                player.name = reader["name"].ToString();
                player.gamesPlayed = Convert.ToInt32(reader["played_games"]);
                player.gamesWon = Convert.ToInt32(reader["won_games"]);
                player.gamesLost = Convert.ToInt32(reader["lost_games"]);
                
                tbl.RowGroups[0].Rows.Add(new TableRow());
                TableRow currentRow = tbl.RowGroups[0].Rows[row];
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(player.name))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(player.gamesPlayed.ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(player.gamesWon.ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(player.gamesLost.ToString()))));
                row++;
            }

            connection.Close();
        }

        public static int categorySwich(string category)
        {
            switch(category)
            {
                case "Cars":
                    return 1;
                case "Mountains":
                    return 2;
                case "Movies & series":
                    return 3;
                case "Rivers":
                    return 4;
                case "States":
                    return 5;
                default:
                    return 0;
            }
        }

        public static void updateSavedGame(Game game)
        {
            SqlConnection connection =
               new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();
            SqlCommand command = new SqlCommand("UpdateSavedProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", game.player.name);
            command.Parameters.AddWithValue("@saved", game.id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static bool checkSavedGame(ref Player player, ref int savedId)
        {
            SqlConnection connection =
               new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();
            SqlCommand command = new SqlCommand("GetSavedProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", player._name);
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                savedId = Convert.ToInt32(reader["saved_game"]);
                reader.Close();

                if (savedId != 0)
                {
                    command = new SqlCommand("UpdateSavedProcedure", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", player._name);
                    command.Parameters.AddWithValue("@saved", 0);
                    command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static Game getGameById(int id)
        {
            Game game = new Game();
            SqlConnection connection =
               new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hangman;Integrated Security=False;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            connection.Open();
            SqlCommand command = new SqlCommand("LoadSavedGameProcedure", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = command.ExecuteReader();
           
            if(reader.Read())
            {
                game.player._name = reader["player_name"].ToString();
                game._level = Convert.ToInt32(reader["level"]);
                game._category = reader["category"].ToString();
                game._word = reader["word"].ToString();
                string usedLetters = reader["used_letters"].ToString();
                
                foreach(char letter in usedLetters)
                {
                    game.addUsedLetter(letter.ToString());
                }

                game._progress = Convert.ToInt32(reader["progress"]);
            }
            
            connection.Close();
            return game;
        }
    }
}
