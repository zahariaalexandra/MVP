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
    public partial class GameWindow : Window
    {
        public Game currentGame = new Game();
        bool gameStarted = false;

        public GameWindow()
        {
            InitializeComponent();

            currentGame.drawing.Source = Utils.copyImage(imgProgress.Source);
            Utils.getGame(ref currentGame);
            lblLevel.Content = lblLevel.Content + " " + currentGame.level;
            lblCategory.Content = lblCategory.Content + " " + currentGame.category;
            lblPlayer.Content = lblPlayer.Content + " " + currentGame.player.name;
            lblWord.Content = Utils.transformWord(currentGame.word);
        }

        private void btnLetter_Click(object sender, RoutedEventArgs e)
        {
            if (!gameStarted)
                gameStarted = true;

            Button btnCurrent = sender as Button;
        }

    }
}
