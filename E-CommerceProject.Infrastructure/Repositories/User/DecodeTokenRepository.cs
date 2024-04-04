using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_CommerceProject.Infrastructure.Repositories.User
{
    public class DecodeTokenRepository
    {
        private readonly IConfiguration _config;
        public DecodeTokenRepository(IConfiguration configuration) {
            _config = configuration;
        }
        public async Task<string> DecodeTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                ClaimsPrincipal claimsPrincipal = await Task.Run(() => tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken));

                // Extract the user ID claim from the validated token
                Claim userIdClaim = claimsPrincipal.FindFirst("UserId");

                if (userIdClaim != null)
                {
                    return userIdClaim.Value;
                }
                else
                {
                    throw new InvalidOperationException("User ID claim not found in token.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Token validation failed.", ex);
            }
        }
    }
}
