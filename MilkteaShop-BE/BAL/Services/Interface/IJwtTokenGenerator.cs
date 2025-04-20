namespace BAL.Services.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string fullname, string email, string role);
        string? GetIdClaim(string token);

    }
}
