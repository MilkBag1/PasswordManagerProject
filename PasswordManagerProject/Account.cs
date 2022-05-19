using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerProject
{
    internal class Account
    {
        public string Description { get; set; }
        public string UserId { get; set; }
        public string LoginUrl { get; set; }
        public string AccountNumber { get; set; }

        public Password Password { get; set; }


        public Account()
        {
          new Account();
        }

        public void UpdatePassword()
        {
            Console.WriteLine("Enter new Password");
            string newPassword = Console.ReadLine();


        }
    }
}
