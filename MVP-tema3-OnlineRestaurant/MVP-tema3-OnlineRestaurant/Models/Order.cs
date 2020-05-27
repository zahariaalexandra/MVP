using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_tema3_OnlineRestaurant.Models
{
    public class Order
    {
        int _id;
        User _user;
        List<Product> _products;
        decimal _price;
        int _discountPercent;
        string _address;
        List<string> _state;

        public Order()
        {
            Id = 0;
            User = new User();
            Products = new List<Product>();
            Price = 0.0m;
            DiscountPercent = 0;
            Address = "";
            State.Add("In progress");
            State.Add("Delivered");
            State.Add("Canceled");
        }

        public int Id { get { return _id; } set { _id = value; } }
        public User User { get { return _user; } set { _user = value; } }
        public List<Product> Products { get { return _products; } set { _products = value; } }
        public decimal Price { get { return _price; } set { _price = value; } }
        public decimal FinalPrice
        { 
            get 
            {
                if (DiscountPercent != 0)
                    return ((DiscountPercent / 100) * Price);

                return Price;
            }
        }
        public int DiscountPercent { get { return _discountPercent; } set { _discountPercent = value; } }
        public string Address { get { return _address; } set { _address = value; } }
        public List<string> State { get { return _state; } set { _state = value; } }
    }
}
