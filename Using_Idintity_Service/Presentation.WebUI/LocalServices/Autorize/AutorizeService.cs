using Identity.Client.Services.ApiRequest;
using Microsoft.AspNetCore.Http;
using Presentation.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.WebUI.LocalServices.Autorize
{
    public class AutorizeService : IAutorizeService
    {
        private readonly IApiRequestService apiRequestService;

        public AutorizeService(IApiRequestService apiRequestService)
        {
            this.apiRequestService = apiRequestService;
        }
        public bool AllowAccess(HttpContext context, string userName, List<string> Permissions)
        {
            var result = apiRequestService.RequestGet<List<string>>($"api/v1/Authentication/GetAllPermission?userName={userName}").Result;
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
