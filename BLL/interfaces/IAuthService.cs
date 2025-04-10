// using server.responses;

using bll.dto;

namespace bll.interfaces
{
    public interface IAuthService
    {
        VerifyTokenRes VerifyTokenAsync(string token, bool isAdmin);
        string GenerateJwtToken(string user_id, bool isAdmin);
    }
}