using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MVP_tema2_Hangman
{
    public partial class NewUserWindow : Window
    {
        public NewUserWindow()
        {
            InitializeComponent();
        }

        byte[] image;

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtName.Foreground = new SolidColorBrush(Colors.SaddleBrown);
            txtName.FontStyle = FontStyles.Normal;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Utils.addNewPlayer(txtName.Text, image);
            this.Close();
        }

        private void txtName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtName.Text = "";
        }

        private void btnChoosePicture_Click(object sender, RoutedEventArgs e)
        {
            bool enabled = false;
            string fileName = "";
            Utils.chooseImage(ref enabled, ref image, ref fileName);
            btnAdd.IsEnabled = enabled;
            lblImage.Content = fileName;
        }
    }
}
