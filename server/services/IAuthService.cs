using server.responses;

namespace server.services
{
    public interface IAuthService
    {
        VerifyTokenRes VerifyTokenAsync(string token, bool isAdmin);
        string GenerateJwtToken(string user_id, bool isAdmin);
    }
}