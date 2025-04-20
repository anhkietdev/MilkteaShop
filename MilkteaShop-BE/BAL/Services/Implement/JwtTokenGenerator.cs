using BAL.Services.Interface;

namespace BAL.Services.Implement
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(string userId, string fullname, string email, string role)
        {
            throw new NotImplementedException();
        }

        public string? GetIdClaim(string token)
        {
            throw new NotImplementedException();
        }
    }
}
