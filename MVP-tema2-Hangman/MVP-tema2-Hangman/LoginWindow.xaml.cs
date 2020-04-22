using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
            Utils.getNames(ref listBoxPlyers);
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.ShowDialog();
            Utils.getNames(ref listBoxPlyers);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void listBoxPlyers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxPlyers.SelectedItem.ToString() != "Ale")
                btnDeleteUser.IsEnabled = true;
            
            btnPlay.IsEnabled = true;
            Utils.changeImage(listBoxPlyers, ref imgProfile);
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Utils.deleteUser(ref listBoxPlyers);
            Utils.getNames(ref listBoxPlyers);
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
