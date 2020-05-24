using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MVP_tema3_OnlineRestaurant.Models
{
    public class Product
    {
        string _name;
        decimal _price;
        Category _category;
        decimal _quantity;
        uint _totalQuantity;
        BitmapImage _photo;

        public Product()
        {
            _name = "";
            _price = 0.0m;
            _category = Category.Appetizers;
            _quantity = 0.0m;
            _totalQuantity = 0;
            _photo = new BitmapImage();
        }

        string Name { get { return _name; } set { _name = value; } }
        decimal Price { get { return _price; } set { _price = value; } }
        Category Category { get { return _category; } set { _category = value; } }
        decimal Quantity { get { return _quantity; } set { _quantity = value; } }
        uint TotalQuantity { get { return _totalQuantity; } set { _totalQuantity = value; } }
        BitmapImage Photo { get { return _photo; } set { _photo = value; } }
    }
}
