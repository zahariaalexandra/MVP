using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace MVP_tema3_OnlineRestaurant
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        int id;

        public MenuWindow()
        {
            InitializeComponent();
        }

        public MenuWindow(int? id, Status status)
        {
            InitializeComponent();
            listCategories.SelectedIndex = 0;

            if(id != null)
                this.id = Convert.ToInt32(id);
            else
            {
                btnAdd.Visibility = Visibility.Hidden;
                btnCart.Visibility = Visibility.Hidden;
                btnProfile.Visibility = Visibility.Hidden;

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
                MessageBox.Show(ConfigurationManager.AppSettings["btnCancelMessage"],
                    "",
                    MessageBoxButton.YesNo);

            if(result == MessageBoxResult.Yes)
            {
                LoginWindow window = new LoginWindow();
                window.Show();
                Close();
            }            
        }
    }
}
