using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Presentation.WebUI.LocalServices.Autorize
{
    public interface IAutorizeService
    {
        bool AllowAccess(HttpContext context, string userName, List<string> Permissions);
    }
}