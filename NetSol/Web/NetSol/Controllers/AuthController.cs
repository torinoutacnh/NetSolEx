using Core.Constant;
using Core.JWT.JWTModel;
using Core.JWT.JWTProcess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetSol.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route(Endpoints.AuthEndpoint.BaseEndpoint)]
        public IActionResult Index()
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var token = JWTUtils.CreateToken(authClaims);
            var refreshToken = JWTUtils.GenerateRefreshToken();
            return Ok(new TokenModel()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            });
        }

        [HttpPost]
        [Route(Endpoints.AuthEndpoint.PostEndpoint)]
        public IActionResult RefreshToken([FromBody]TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;

            var principal = JWTUtils.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = JWTUtils.CreateToken(principal.Claims.ToList());
            var newRefreshToken = JWTUtils.GenerateRefreshToken();

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo
            });
        }
    }
}
