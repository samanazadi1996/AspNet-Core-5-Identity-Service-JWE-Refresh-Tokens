using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.WebUI.Infrastructure.Authentication.Services.Autorize
{
    public class AutorizeService : IAutorizeService
    {
        public bool AllowAccess(HttpContext context, string userName, List<string> Permissions)
        {
            var result = ApiRequestExtention.RequestGet<List<string>>(context, $"api/v1/Authentication/GetAllPermission?userName={userName}");
            if (result.IsSuccess)
            {
                foreach (var item in Permissions)
                {
                    if (!result.Data.Any(r => r.Equals(item)))
                        return false;
                }
            }
            return true;
        }
    }
}
