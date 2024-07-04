using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zakazivanje.Classes
{
    public class Customer
    {
        public Customer()
        {

        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }


        public void AssignValues(string id, string name, string lastName, string email, string phone, string address, string password)
        {
            this.Id = id;
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.Address = address;
            this.Password = password;
        }

        private string id;
        private string name;
        private string lastName;
        private string email;
        private string password;
        private string phone;
        private string address;
    }
}
