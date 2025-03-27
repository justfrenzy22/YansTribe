using server.responses;

namespace server.services
{
    public interface IAuthService
    {
        VerifyTokenRes VerifyTokenAsync(string token);
        // Task<string> GenerateTokenAsync(string username, string password);
        // Task<bool> RegisterUserAsync(string username, string email, string password);
        string GenerateJwtToken(string user_id);
    }
}