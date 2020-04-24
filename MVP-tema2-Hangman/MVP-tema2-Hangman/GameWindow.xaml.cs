﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            lblLevel.Content = lblLevel.Content + " " + currentGame._level;
            lblCategory.Content = lblCategory.Content + " " + currentGame._category;
            lblPlayer.Content = lblPlayer.Content + " " + currentGame._player._name;
            txtWord.Text = Utils.transformWord(currentGame._word);
        }

        private void btnLetter_Click(object sender, RoutedEventArgs e)
        {
            Button btnCurrent = sender as Button;
            bool gameWon = false;
            string txt = txtWord.Text;
            BitmapImage img = new BitmapImage();
            bool gameLost = Utils.letterTest(btnCurrent.Content.ToString(), ref currentGame, ref txt, ref img, ref gameWon);
            btnCurrent.IsEnabled = false;
            txtWord.Text = txt;
            imgProgress.Source = img;


            if (gameLost || gameWon)
            {
                Panel container = (Panel)this.Content;
                UIElementCollection elementCollection = container.Children;
                List<FrameworkElement> elementList = elementCollection.Cast<FrameworkElement>().ToList();
                var buttons = elementList.OfType<Button>();

                foreach (Button button in buttons)
                {
                    button.IsEnabled = false;
                }

                if (gameWon && currentGame._level < 5)
                {
                    imgProgress.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameWon.png"));
                    txtWord.Foreground = new SolidColorBrush(Colors.DarkSeaGreen);
                    MessageBox.Show("Level finished!", "", MessageBoxButton.OK);
                    currentGame._level++;
                    Utils.initializeGame(ref currentGame, 0);
                    currentGame._progress = 0;
                    Utils.addGame(currentGame, false);
                    GameWindow newGameWindow = new GameWindow();
                    newGameWindow.Show();
                    this.Close();
                }
                else if (gameWon && currentGame._level == 5)
                {
                    imgProgress.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameWon.png"));
                    txtWord.Foreground = new SolidColorBrush(Colors.DarkSeaGreen);
                    MessageBox.Show("Game won!", "", MessageBoxButton.OK);
                    Utils.updatePlayer(currentGame._player._name, true);
                    LoginWindow newWindow = new LoginWindow();
                    newWindow.Show();
                    this.Close();
                }
                else
                {
                    imgProgress.Source = new BitmapImage(new Uri("pack://application:,,,/MVP-tema2-Hangman;component/progressImages/ImgGameLost.png"));
                    txtWord.Foreground = new SolidColorBrush(Colors.MediumVioletRed);

                    if (MessageBox.Show("Game lost. \nDo you want to start a new game?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Game newGame = new Game(currentGame._player);
                        newGame._level = 1;
                        Utils.initializeGame(ref newGame, 0);
                        newGame._usedLetters = new List<string>();
                        Utils.addGame(newGame, false);

                        GameWindow newGameWindow = new GameWindow();
                        newGameWindow.Show();
                    }
                    else
                    {
                        LoginWindow newWindow = new LoginWindow();
                        newWindow.Show();
                    }

                    Utils.updatePlayer(currentGame._player._name, false);
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
            Player player = new Player(currentGame._player._name);
            Game newGame = new Game(player);
            Utils.initializeGame(ref newGame, Utils.categorySwich(currentGame._category));
            newGame._level = 1;
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
    }
}
