using Common;
using Microsoft.Extensions.Options;
using Moq;
using Services.CryptographyDomain.Implementation;
using Xunit;

namespace Services_Tests.Cryptography
{
    public class EncryptServiceTest
    {
        [Theory]
        [InlineData("www.google.com", "Ds2C7fpNg8nfrl1TDwY6eA==")]
        [InlineData("https://stackoverflow.com/", "7h0woQ/HIQgzws6I5PFRTekiHgIwfnprTRjB5ZBoqiE=")]
        public void Test1(string url, string result)
        {
            var Key = "Use 32 Characters For Encryption";

            var moqSiteSettings = new Mock<IOptionsSnapshot<SiteSettings>>();
            var siteSettings = new SiteSettings() { EncryptionSettings = new EncryptionSettings() { Key = Key } };

            moqSiteSettings.Setup(x => x.Value).Returns(siteSettings);

            var service = new EncryptService(moqSiteSettings.Object);
            var resultService = service.Encrypt(url);
            Assert.Equal(resultService, result);

        }

    }
}
