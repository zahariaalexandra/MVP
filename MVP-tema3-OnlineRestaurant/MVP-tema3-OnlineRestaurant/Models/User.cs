using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP_tema3_OnlineRestaurant.Models
{
    class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }

        public User()
        {
            Id = 0;
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
            Telephone = "";
            Address = "";
            Status = Status.Customer;
        }
    }
}
