using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
            => httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static string GetUserName(this HttpContext httpContext)
            => httpContext.User.Identity.Name;
    }
}
