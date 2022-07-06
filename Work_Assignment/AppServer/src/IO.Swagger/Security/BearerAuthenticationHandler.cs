using System;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static IO.Swagger.Security.TokenDetails;

namespace IO.Swagger.Security
{
    /// <summary>
    /// class to handle bearer authentication.
    /// </summary>
    public class BearerAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// scheme name for authentication handler.
        /// </summary>
        public const string SchemeName = "Bearer";

        public BearerAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// verify that require authorization header exists.
        /// </summary>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            AuthenticationHeaderValue authHeader = null;
            ClaimsPrincipal principal = null;

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }
            try
            {
                authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                principal = TokensManager.validateToken(authHeader.Parameter);

                if (principal == null)
                {
                    return AuthenticateResult.Fail("Invalid Authorization Header");
                }
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (this.Request.Path.Equals("/users/logout"))
            {
                ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;
                Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
                string username = usernameClaim.Value;

                TokensManager.blackListToken(username, authHeader.Parameter);
            }

            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

        }
    }
}
