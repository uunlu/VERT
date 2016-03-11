using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VERT.Core.Users
{
    public class User : RootEntity
    {
        public User()
        {
        }

        public static User Register(string name, string email, string password)
        {
            var user = new User
            {
                Name = name,
                Email = email,
            };
            user.PasswordSaltKey = EncriptionUtil.GenerateSaltKey();
            user.PasswordHash = EncriptionUtil.GeneratePasswordHash(password, user.PasswordSaltKey);
            return user;
        }

        public string Name { get; set; }
        public string Email { get; private set; }
        public string PasswordSaltKey { get; private set; }
        public string PasswordHash { get; private set; }

        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (!IsPasswordValid(currentPassword))
                throw new CoreException("Invalid credentials.");

            PasswordHash = EncriptionUtil.GeneratePasswordHash(newPassword, PasswordSaltKey);
        }

        public bool IsPasswordValid(string password)
        {
            var passwordHashToCheck = EncriptionUtil.GeneratePasswordHash(password, PasswordSaltKey);
            return passwordHashToCheck == PasswordHash;
        }
    }
}
