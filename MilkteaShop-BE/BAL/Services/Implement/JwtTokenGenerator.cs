using DAL.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BAL.Services.Implement
{
    public static class JwtTokenGenerator 
    {
        public static string GenerateToken(this User user, string secretKey, int expiredMinutes, string issuer, string audience)
        {
            var secretKeyInByte = Encoding.UTF8.GetBytes(secretKey);
            var sercurityKey = new SymmetricSecurityKey(secretKeyInByte);
            var credentials = new SigningCredentials(sercurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken
                (
                    issuer: issuer,
                    audience: audience,
                    expires: DateTime.UtcNow.AddMinutes(expiredMinutes),
                    claims: claims,
                    signingCredentials: credentials
                );

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.WriteToken(token);
            foreach (var claim in token.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            return jwtToken;
        }

        public static string? GetIdClaim(string token)
        {
            throw new NotImplementedException();
        }
    }
}
