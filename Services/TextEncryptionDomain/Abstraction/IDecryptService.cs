namespace Services.TextEncryptionDomain.Abstraction
{
    public interface IDecryptService
    {
        string Decrypt(string text);
    }
}