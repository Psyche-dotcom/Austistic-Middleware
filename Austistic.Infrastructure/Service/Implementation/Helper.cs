using Austistic.Infrastructure.Service.Interface;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Austistic.Infrastructure.Service.Implementation
{
    public class Helper : IHelper
    {
        private readonly byte[] _key;
        private readonly IConfiguration _configuration;
        public Helper(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = Encoding.UTF8.GetBytes(_configuration["EncryptionSettings:Key"]);
            _configuration = configuration;
        }

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

        public string GenerateOtp(int numberOfDigits)
        {
            if (numberOfDigits <= 0)
            {
                throw new ArgumentException("Number of digits must be greater than zero.");
            }

            byte[] randomBytes = new byte[numberOfDigits];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert the random bytes to a numeric string
            StringBuilder otp = new StringBuilder(numberOfDigits);
            for (int i = 0; i < numberOfDigits; i++)
            {
                // Ensure each byte is a digit (0-9)
                byte digit = (byte)(randomBytes[i] % 10);
                otp.Append(digit.ToString());
            }

            return otp.ToString();
        }
        public string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
            RandomNumberGenerator.Fill(nonce);

            byte[] cipherText;
            byte[] tag;

            using (AesGcm aesGcm = new AesGcm(_key))
            {
                byte[] encryptedData = new byte[plainTextBytes.Length];
                byte[] tagBuffer = new byte[AesGcm.TagByteSizes.MaxSize];

                aesGcm.Encrypt(nonce, plainTextBytes, encryptedData, tagBuffer);

                cipherText = new byte[nonce.Length + encryptedData.Length + tagBuffer.Length];
                Buffer.BlockCopy(nonce, 0, cipherText, 0, nonce.Length);
                Buffer.BlockCopy(encryptedData, 0, cipherText, nonce.Length, encryptedData.Length);
                Buffer.BlockCopy(tagBuffer, 0, cipherText, nonce.Length + encryptedData.Length, tagBuffer.Length);
            }

            return Convert.ToBase64String(cipherText);
        }

        public string Decrypt(string encryptedText)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedText);

            byte[] nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
            byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];
            byte[] encryptedData = new byte[cipherText.Length - nonce.Length - tag.Length];

            Buffer.BlockCopy(cipherText, 0, nonce, 0, nonce.Length);
            Buffer.BlockCopy(cipherText, nonce.Length, encryptedData, 0, encryptedData.Length);
            Buffer.BlockCopy(cipherText, nonce.Length + encryptedData.Length, tag, 0, tag.Length);

            byte[] decryptedData;

            using (AesGcm aesGcm = new AesGcm(_key))
            {
                decryptedData = new byte[encryptedData.Length];
                aesGcm.Decrypt(nonce, encryptedData, tag, decryptedData);
            }

            return Encoding.UTF8.GetString(decryptedData);
        }
    }

}
