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

            this.id = Convert.ToInt32(id);
        }
    }
}
