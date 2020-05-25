﻿using System;
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
        int _id;
        string _name;
        decimal _price;
        string _category;
        uint _quantity;
        uint _totalQuantity;
        BitmapImage _photo;

        public Product()
        {
            _id = 0;
            _name = "";
            _price = 0.0m;
            _category = "";
            _quantity = 0;
            _totalQuantity = 0;
            _photo = new BitmapImage();
        }

        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public decimal Price { get { return _price; } set { _price = value; } }
        public string Category { get { return _category; } set { _category = value; } }
        public uint Quantity { get { return _quantity; } set { _quantity = value; } }
        public uint TotalQuantity { get { return _totalQuantity; } set { _totalQuantity = value; } }
        public BitmapImage Photo { get { return _photo; } set { _photo = value; } }
        public string Info { get { return (Price.ToString() + "lei / " + Quantity.ToString()) + "g"; } }
    }
}
