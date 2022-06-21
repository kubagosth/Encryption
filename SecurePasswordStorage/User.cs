using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePasswordStorage
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public int LoginAttempts { get; set; }
        public bool AccountLocked { get; set; }

        public User(string username)
        {
            Username = username;
            AccountLocked = false;
        }
    }
}
