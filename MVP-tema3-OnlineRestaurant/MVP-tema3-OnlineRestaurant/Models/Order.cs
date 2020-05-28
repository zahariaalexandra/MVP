using System;
using System.Collections.Generic;
using System.Configuration;
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
        string _status;
        DateTime _startDate;
        DateTime _finishDate;
        decimal _price;
        uint _shipping;
        uint _discount;
        decimal _finalPrice;

        public Order()
        {
            _id = 0;
            _user = new User();
            _products = new List<Product>();
            _status = "";
            _startDate = new DateTime();
            _finishDate = new DateTime();
            _price = 0.0m;
            _shipping = 0;
            _discount = 0;
            _finalPrice = 0;
        }

        public int Id { get { return _id;  } set { _id = value; } }
        public User User { get { return _user;  } set { _user = value; } }
        public List<Product> Products { get { return _products;  } set { _products = value; } }
        public string Status { get { return _status;  } set { _status = value; } }
        public DateTime StartDate { get { return _startDate;  } set { _startDate = value; } }
        public DateTime FinishDate { get { return _finishDate;  } set { _finishDate = value; } }
        public decimal Price { get { return _price;  } set { _price = value; } }

        public uint Shipping
        { 
            set 
            {
                if (Price < 50)
                    _shipping = Convert.ToUInt32(ConfigurationManager.AppSettings["shippingHigh"]);
                else if(Price >= 50 && Price < 100)
                    _shipping = Convert.ToUInt32(ConfigurationManager.AppSettings["shippingLow"]);
                else
                    _shipping = Convert.ToUInt32(ConfigurationManager.AppSettings["shippingFree"]);
            }

            get
            {
                return _shipping;
            }
        }
        
        public uint Discount
        {
            set
            {
                if (Price >= 150)
                    _discount = Convert.ToUInt32(ConfigurationManager.AppSettings["discount"]);
                else
                    _discount = Convert.ToUInt32(ConfigurationManager.AppSettings["noDiscount"]);
            }

            get
            {
                return _discount;
            }
        }

        public decimal FinalPrice
        {
            set
            {
                _finalPrice = Price + Shipping - Discount;
            }

            get
            {
                return _finalPrice;
            }
        }
    }
}
