using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Helpers
{
    public static class PasswordHelper
    {
        public static (string Hash, string Salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = Convert.ToBase64String(hmac.Key);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = hmac.ComputeHash(passwordBytes);
            var hash = Convert.ToBase64String(hashBytes);
            return (Hash: hash, Salt: salt);
        }

        public static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            using var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt));
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var computedHashBytes = hmac.ComputeHash(passwordBytes);
            var computedHash = Convert.ToBase64String(computedHashBytes);
            return computedHash == storedHash;
        }
    }
}
