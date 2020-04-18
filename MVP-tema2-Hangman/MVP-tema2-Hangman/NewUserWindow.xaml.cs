﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
        {
            InitializeComponent();
        }

        string image = "";

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtName.Foreground = new SolidColorBrush(Colors.SaddleBrown);
            txtName.FontStyle = FontStyles.Normal;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Utils.addNewPlayer(txtName, image);
            this.Close();
        }

        private void txtName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtName.Text = "";
        }

        private void btnPicture_Click(object sender, RoutedEventArgs e)
        {
            btnAdd.IsEnabled = true;
            Button button = sender as Button;
            Image temp = button.Content as Image;
            image = temp.Source.ToString();
        }
    }
}
