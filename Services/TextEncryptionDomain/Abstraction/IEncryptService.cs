namespace Services.TextEncryptionDomain.Abstraction
{
    public interface IEncryptService
    {
        string Encrypt(string text);
    }
}