using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using Panel = System.Windows.Controls.Panel;

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

            Utils.getGame(ref currentGame);
            lblLevel.Content = lblLevel.Content + " " + currentGame.level;
            lblCategory.Content = lblCategory.Content + " " + currentGame.category;
            lblPlayer.Content = lblPlayer.Content + " " + currentGame.player.name;
            txtWord.Text = Utils.transformWord(currentGame.word);
        }

        private void btnLetter_Click(object sender, RoutedEventArgs e)
        {
            if (!gameStarted)
                gameStarted = true;

            Button btnCurrent = sender as Button;
            bool gameWon = false;
            bool gameLost = Utils.letterTest(btnCurrent, ref currentGame, ref txtWord, ref imgProgress, ref gameWon);

            if(gameLost || gameWon)
            {
                Panel container = (Panel)this.Content;
                UIElementCollection elementCollection = container.Children;
                List<FrameworkElement> elementList = elementCollection.Cast<FrameworkElement>().ToList();
                var buttons = elementList.OfType<Button>();

                foreach (Button button in buttons)
                {
                    button.IsEnabled = false;
                }

                if (gameWon && currentGame.level < 5)
                {
                    imgProgress.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameWon.png"));
                    txtWord.Foreground = new SolidColorBrush(Colors.DarkSeaGreen);
                    MessageBox.Show("Level finished!", "", MessageBoxButton.OK);
                    currentGame.level++;
                    Utils.initializeGame(ref currentGame, 0);
                    Utils.addGame(currentGame);
                    GameWindow newGameWindow = new GameWindow();
                    newGameWindow.Show();
                    this.Close();
                }
                else if(gameWon && currentGame.level == 5)
                {
                    imgProgress.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameWon.png"));
                    txtWord.Foreground = new SolidColorBrush(Colors.DarkSeaGreen);
                    MessageBox.Show("Game won!", "", MessageBoxButton.OK);
                    Utils.updatePlayer(currentGame.player.name, true);                       
                    LoginWindow newWindow = new LoginWindow();
                    newWindow.Show();                   
                    this.Close();
                }
                else
                {
                    imgProgress.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameLost.png"));
                    txtWord.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                   
                    if(MessageBox.Show("Game lost. \nDo you want to start a new game?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Game newGame = new Game(currentGame.player);
                        newGame.level = 1;
                        Utils.initializeGame(ref newGame, 0);
                        newGame.usedLetters = new List<string>();
                        Utils.addGame(newGame);

                        GameWindow newGameWindow = new GameWindow();
                        newGameWindow.Show();
                    }
                    else
                    {
                        LoginWindow newWindow = new LoginWindow();
                        newWindow.Show();
                    }

                    Utils.updatePlayer(currentGame.player.name, false);
                    this.Close();
                }
            }
        }

    }
}
