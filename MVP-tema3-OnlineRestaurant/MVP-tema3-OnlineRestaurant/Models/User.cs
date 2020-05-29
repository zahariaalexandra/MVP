using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MVP_tema3_OnlineRestaurant.Models
{
    public class User
    {
        private int _id;
        private string _firstName;
        private string _lastname;
        private string _email;
        private string _password;
        private string _telephone;
        private string _address;
        private Status _status;

        public int Id { get { return _id; } set { _id = value; } }
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        public string LastName { get { return _lastname; } set { _lastname = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public string Telephone { get { return _telephone; } set { _telephone = value; } }
        public string Address { get { return _address; } set { _address = value; } }
        public Status Status { get { return _status; } set { _status = value; } }

        public string FullName { get { return (FirstName + " " + LastName); } }

        public User()
        {
            _id = 0;
            _firstName = "";
            _lastname = "";
            _email = "";
            _password = "";
            _telephone = "";
            _address = "";
            _status = Status.CUSTOMER;
        }
    }
}
