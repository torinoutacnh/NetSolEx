using Core.JWT.JWTProcess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.JWT.LoadSetting
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeJWTAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // authorization
            if (token == null)
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

            var expClaim = JWTUtils.GetPrincipalFromExpiredToken(token).Claims.First(x => x.Type == "exp")?.Value;

            long expSec = 0;
            long.TryParse(expClaim, out expSec);

            var tokenExpiryTime = DateTimeOffset.FromUnixTimeSeconds(expSec);
            if (tokenExpiryTime < DateTime.UtcNow)
            {
                context.Result = new JsonResult(new { message = "Out date" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
