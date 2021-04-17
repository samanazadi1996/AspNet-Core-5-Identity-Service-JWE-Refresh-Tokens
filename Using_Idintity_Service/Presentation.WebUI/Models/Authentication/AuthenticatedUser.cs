namespace Presentation.WebUI.Models.Authentication
{
    public class AuthenticatedUser
    {
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
    }
}
