using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace HKSH.Common.Helper
{
    /// <summary>
    /// jwt token parse helper
    /// </summary>
    public static class JwtHelper
    {
        /// <summary>
        /// find user id
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetUserId(string token)
        {
            if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = token.Substring(7);
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            var subClaim = claims.FirstOrDefault(c => c.Type == "sub");
            if (subClaim != null)
            {
                var sub = subClaim.Value;
                int providerIndex = sub.IndexOf(':', 2);
                string externalId = sub[(providerIndex + 1)..];
                return externalId;
            }

            return "";
        }
    }
}