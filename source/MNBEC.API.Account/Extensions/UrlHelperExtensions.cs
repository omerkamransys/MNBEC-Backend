using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using MNBEC.API.Account.Controllers;

namespace MNBEC.API.Account.Extensions
{
    public static class UrlHelperExtensions
    {
        
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, int userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: string.Empty,
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, int userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: string.Empty,
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        

    }
}
