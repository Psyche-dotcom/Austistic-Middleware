using Austistic.Infrastructure.Service.Interface;
using System.Security.Cryptography;
using System.Text;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class Helper : IHelper
    {

        public string GenerateSecureRandomAlphanumeric(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            if (length <= 0)
                throw new ArgumentException("Length must be greater than 0.", nameof(length));

            StringBuilder result = new StringBuilder(length);
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];

                rng.GetBytes(randomBytes);

                for (int i = 0; i < length; i++)
                {
                    result.Append(chars[randomBytes[i] % chars.Length]);
                }
            }

            return result.ToString();
        }
    }
}
