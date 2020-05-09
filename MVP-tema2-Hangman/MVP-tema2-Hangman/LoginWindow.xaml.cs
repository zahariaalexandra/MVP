using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
namespace MVP_tema2_Hangman
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            List<string> namesList = new List<string>();
            Utils.getNames(ref namesList);
            listBoxPlyers.ItemsSource = namesList;
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.ShowDialog();
            List<string> namesList = new List<string>();
            Utils.getNames(ref namesList);
            listBoxPlyers.ItemsSource = namesList;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void listBoxPlyers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listBoxPlyers.SelectedIndex != -1)
            {
                btnDeleteUser.IsEnabled = true;
                btnPlay.IsEnabled = true;
                string selectedItem = listBoxPlyers.SelectedItem.ToString();
                BitmapImage source = new BitmapImage();
                Utils.changeImage(selectedItem, ref source);
                imgProfile.Source = source;
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = listBoxPlyers.SelectedItem.ToString();
            Utils.deleteUser(selectedItem);
            listBoxPlyers.SelectedIndex = -1;
            List<string> namesList = new List<string>();
            Utils.getNames(ref namesList);
            listBoxPlyers.ItemsSource = namesList;
            btnDeleteUser.IsEnabled = false;
            btnPlay.IsEnabled = false;
            imgProfile.Source = null;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Player player = new Player(listBoxPlyers.SelectedItem.ToString());
            Game game = new Game(player);
            Utils.initializeGame(ref game, 0);
            game.level = 1;
            Utils.addGame(game);

            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }
    }
}
