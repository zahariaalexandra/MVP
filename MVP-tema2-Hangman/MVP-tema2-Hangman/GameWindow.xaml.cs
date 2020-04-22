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

                if (gameWon)
                {
                    imgProgress.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameWon.png"));
                    txtWord.Foreground = new SolidColorBrush(Colors.DarkSeaGreen);
                    MessageBox.Show("Game won!", "", MessageBoxButton.OK);
                }
                else
                {
                    imgProgress.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameLost.png"));
                    txtWord.Foreground = new SolidColorBrush(Colors.MediumVioletRed);
                    MessageBox.Show("Game lost!", "", MessageBoxButton.OK);
                }
            }
        }

    }
}
