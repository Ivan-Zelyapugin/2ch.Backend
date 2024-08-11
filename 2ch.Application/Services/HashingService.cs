using _2ch.Application.Interfaces;
using _2ch.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace _2ch.Application.Services
{
    public class HashingService : IHashingService
    {
        public string GenerateDeterministicSalt(string ipAddress)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(ipAddress));
                return Convert.ToBase64String(saltBytes);
            }
        }

        public string GenerateHash(string ipAddress, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var combinedString = $"{ipAddress}{salt}";
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
