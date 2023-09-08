using System.IdentityModel.Tokens.Jwt;

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

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
            IEnumerable<System.Security.Claims.Claim> claims = jwtToken.Claims;
            System.Security.Claims.Claim? subClaim = claims.FirstOrDefault(c => c.Type == "sub");
            if (subClaim != null)
            {
                string sub = subClaim.Value;
                int providerIndex = sub.IndexOf(':', 2);
                string externalId = sub[(providerIndex + 1)..];
                return externalId;
            }

            return "";
        }
    }
}