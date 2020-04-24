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
                    currentGame.progress = 0;
                    Utils.addGame(currentGame, false);
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
                        Utils.addGame(newGame, false);

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

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            Player player = new Player(currentGame.player.name);
            Game newGame = new Game(player);
            Utils.initializeGame(ref newGame, Utils.categorySwich(currentGame.category));
            newGame.level = 1;
            Utils.addGame(newGame, false);
            GameWindow window = new GameWindow();
            window.Show();
            this.Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            string instructions = "The goal is to guess all letters contained by the word. \nOnce you pressed a letter it becomes used. If it was right, it will be revealed in the word, otherwise a new body part will be added to the drawing. \nIf the word is completed before the drawing is finished, the level is won and the next level will appear. \nIf the drawing is completed before the word is guessed, the game is lost.";
            MessageBox.Show(instructions, "Game instructions", MessageBoxButton.OK);
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Zaharia Alexandra\nGrupa 283\nInformatica", "Info", MessageBoxButton.OK);
        }

        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            Utils.addGame(currentGame, true);
            Game savedGame = new Game();
            Utils.getGame(ref savedGame);
            Utils.updateSavedGame(savedGame);
            MessageBox.Show("The game is saved!", "", MessageBoxButton.OK);
        }

        private void OpenGame_Click(object sender, RoutedEventArgs e)
        {
            int savedGameId = 0;
            bool saved = Utils.checkSavedGame(ref currentGame.player, ref savedGameId);

            if(saved)
            {
                if(MessageBox.Show("Are you sure you want to load your saved game?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Game savedGame = Utils.getGameById(savedGameId);
                    Utils.addGame(savedGame, false);
                    GameWindow gameWindow = new GameWindow();
                    gameWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("You don't have any saved games", "", MessageBoxButton.OK);
            }
        }
    }
}
