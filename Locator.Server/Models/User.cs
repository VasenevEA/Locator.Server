
using System;

namespace Locator.API
{
    public class User
    {
        public int ID { get; set; }
        public int GroupID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        //Auth part
        public string Token { get; set; }
        public string Key { get; set; }
        public DateTime EndDT { get; set; }
    }

}
