namespace Services.CryptographyDomain.Abstraction
{
    public interface IEncryptService
    {
        string Encrypt(string text);
    }
}