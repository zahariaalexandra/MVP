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
        OrderProgress _status;
        DateTime _startDate;
        DateTime _finishDate;
        decimal _price;
        string _address;

        public Order()
        {
            _id = 0;
            _user = new User();
            _products = new List<Product>();
            _status = OrderProgress.IN_PROGRESS;
            _startDate = new DateTime();
            _finishDate = new DateTime();
            _price = 0.0m;
            _address = "";
        }

        public int Id { get { return _id;  } set { _id = value; } }
        public User User { get { return _user;  } set { _user = value; } }
        public List<Product> Products { get { return _products;  } set { _products = value; } }
        public OrderProgress Status { get { return _status;  } set { _status = value; } }
        public DateTime StartDate { get { return _startDate;  } set { _startDate = value; } }
        public DateTime FinishDate { get { return _finishDate;  } set { _finishDate = value; } }
        public decimal Price { get { return _price;  } set { _price = value; } }
        public string Address { get { return _address; } set { _address = value; } }

        public uint Shipping
        { 
            get
            {
                if (Price < 50)
                    return Convert.ToUInt32(ConfigurationManager.AppSettings["shippingHigh"]);
                else if (Price >= 50 && Price < 100)
                    return Convert.ToUInt32(ConfigurationManager.AppSettings["shippingLow"]);
                else
                    return Convert.ToUInt32(ConfigurationManager.AppSettings["shippingFree"]);
            }
        }
        
        public uint Discount
        {
            get
            {
                if (Price >= 150)
                    return Convert.ToUInt32(ConfigurationManager.AppSettings["discount"]);
                else
                    return Convert.ToUInt32(ConfigurationManager.AppSettings["noDiscount"]);
            }
        }

        public decimal FinalPrice
        {
            get
            {
                return (Price + Shipping - Discount);
            }
        }
    }
}
