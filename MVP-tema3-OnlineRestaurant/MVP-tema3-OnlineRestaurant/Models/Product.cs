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
        //uint _quantityCommand;

        public Product()
        {
            _name = "";
            _price = 0.0m;
            _category = Category.Appetizers;
            _quantity = 0.0m;
            _totalQuantity = 0;
            _photo = new BitmapImage();
            //_quantityCommand = 0;
        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
>>>>>>> Stashed changes
        string Name { get { return _name; } set { _name = value; } }
        decimal Price { get { return _price; } set { _price = value; } }
        Category Category { get { return _category; } set { _category = value; } }
        decimal Quantity { get { return _quantity; } set { _quantity = value; } }
        uint TotalQuantity { get { return _totalQuantity; } set { _totalQuantity = value; } }
        BitmapImage Photo { get { return _photo; } set { _photo = value; } }
<<<<<<< Updated upstream
=======
        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public decimal Price { get { return _price; } set { _price = value; } }
        public string Category { get { return _category; } set { _category = value; } }
        public uint Quantity { get { return _quantity; } set { _quantity = value; } }
        public uint TotalQuantity { get { return _totalQuantity; } set { _totalQuantity = value; } }
        public BitmapImage Photo { get { return _photo; } set { _photo = value; } }
        public string Info { get { return (Price.ToString() + "lei / " + Quantity.ToString()) + "g"; } set { Info = value; } }
        public string FullInfo { get { return TotalQuantity.ToString() + "x " + Name; } set { FullInfo = value; } }
        
        //public uint CommandQuantity { get { return _quantityCommand; } set { _quantityCommand = value; } }
>>>>>>> Stashed changes
=======
        
        public string Info { get { return (Price.ToString() + "lei / " + Quantity.ToString()) + "g"; } set { Info = value; } }
        public string FullInfo { get { return TotalQuantity.ToString() + "x " + Name; } set { FullInfo = value; } }
       
>>>>>>> Stashed changes
    }
}
