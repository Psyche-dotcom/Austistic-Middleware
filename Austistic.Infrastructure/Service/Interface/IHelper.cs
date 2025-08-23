namespace Austistic.Infrastructure.Service.Interface
{
    public interface IHelper
    {
        string Encrypt(string plainText);
        string Decrypt(string encryptedText);
        string GenerateOtp(int numberOfDigits);
        string GenerateSecureRandomAlphanumeric(int length);
    }
}
