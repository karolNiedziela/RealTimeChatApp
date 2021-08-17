using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.TagHelpers
{
    [HtmlTargetElement("*", Attributes = "authorization")]
    [HtmlTargetElement("*", Attributes = "authorization-policy")]
    public class AuthorizationTagHelper : TagHelper
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationTagHelper(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HtmlAttributeName("authorization")]
        public bool RequiresAuthentication { get; set; }

        [HtmlAttributeName("authorization-policy")]
        public string RequiredPolicy { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; } 

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var requiresAuthentication = RequiresAuthentication || !string.IsNullOrEmpty(RequiredPolicy);
            var showOutput = false;

            if (context.AllAttributes["authorization"] != null && !requiresAuthentication && !ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                showOutput = true;
            }

            else if (!string.IsNullOrEmpty(RequiredPolicy))
            {
                var authorized = false;
                var cachedResult = ViewContext.ViewData["AuthPolicy." + RequiredPolicy];
                if (cachedResult != null)
                {
                    authorized = (bool)cachedResult;
                }
                else
                {
                    var authorizationResult = await _authorizationService.AuthorizeAsync(ViewContext.HttpContext.User, RequiredPolicy);
                    authorized = authorizationResult.Succeeded;
                    ViewContext.ViewData["AuthPolicy." + "RequiredPolicy"] = authorized;
                }

                showOutput = authorized;
            }
            else if (requiresAuthentication && ViewContext.HttpContext.User.Identity.IsAuthenticated)
            {
                showOutput = true;
            }

            if (!showOutput)
            {
                output.SuppressOutput();
            }
        }
    }
}
