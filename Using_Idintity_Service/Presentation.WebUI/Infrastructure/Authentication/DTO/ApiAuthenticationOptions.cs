namespace Presentation.WebUI.Infrastructure.Authentication.DTO
{
    public class ApiAuthenticationOptions
    {
        public string Domain { get; set; }
        public string LoginPath { get; set; }
    }
    public static class MapOptions
    {
        public static ApiAuthenticationOptions MapApiAuthenticationOptions(this ApiAuthenticationOptions apiAuthenticationOptions, string domain, string loginPath)
        {
            apiAuthenticationOptions.Domain = domain;
            apiAuthenticationOptions.LoginPath = loginPath;

            return apiAuthenticationOptions;
        }

    }
}