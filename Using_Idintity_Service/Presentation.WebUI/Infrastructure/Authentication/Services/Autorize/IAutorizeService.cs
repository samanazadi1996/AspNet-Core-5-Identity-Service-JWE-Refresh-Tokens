using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Presentation.WebUI.Infrastructure.Authentication.Services.Autorize
{
    public interface IAutorizeService
    {
        bool AllowAccess(HttpContext context, string userName, List<string> Permissions);
    }
}