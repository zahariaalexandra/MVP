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

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }

        public string FullName { get { return (FirstName + " " + LastName); } }

        public User()
        {
            Id = 0;
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
            Telephone = "";
            Address = "";
            Status = Status.CUSTOMER;
        }
    }
}
