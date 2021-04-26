using Common;
using Microsoft.Extensions.Options;
using Moq;
using Services.CryptographyDomain.Implementation;
using System;
using Xunit;

namespace Services_Tests
{
    public class DecryptServiceTest
    {
        [Theory]
        [InlineData("Ds2C7fpNg8nfrl1TDwY6eA==", "www.google.com")]
        [InlineData("7h0woQ/HIQgzws6I5PFRTekiHgIwfnprTRjB5ZBoqiE=", "https://stackoverflow.com/")]
        public void Test1(string text, string result)
        {
            var Key = "Use 32 Characters For Encryption";

            var moqSiteSettings = new Mock<IOptionsSnapshot<SiteSettings>>();
            var siteSettings = new SiteSettings() { EncryptionSettings = new EncryptionSettings() { Key = Key } };

            moqSiteSettings.Setup(x => x.Value).Returns(siteSettings);

            var service = new DecryptService(moqSiteSettings.Object);
            var resultService = service.Decrypt(text);
            Assert.Equal(resultService, result);

        }
    }
}
