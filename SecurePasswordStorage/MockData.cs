using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePasswordStorage
{
    public class MockData
    {
        private List<User> users;

        public MockData()
        {
            this.users = new List<User>();

            string password = "test";
            User user = new User("user");
            PBKDF2 encryption = new PBKDF2();

            byte[] salt = encryption.GenerateSalt();
            byte[] hashedPassword = encryption.HashPassword(Encoding.UTF8.GetBytes(password), salt, 100);
            user.Password = Convert.ToBase64String(hashedPassword);
            user.Salt = salt;

            this.users.Add(user);
        }
        public List<User> GetMockData()
        {
            return users;
        }

        public int AddLoginAttempt(User user)
        {
            user.LoginAttempts++;
            return user.LoginAttempts;
        }

        public void LockAccount(User user)
        {
            user.AccountLocked = true;
        }

        public void ResetLoginAttempts(User user)
        {
            user.LoginAttempts = 0;
        }
    }
}
