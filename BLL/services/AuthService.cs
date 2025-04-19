using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using bll.interfaces;
using bll.dto;

namespace bll.services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config) => this._config = config;

        public VerifyTokenRes VerifyTokenAsync(string token, bool isAdmin)
        {

            string key = "";
            if (isAdmin)
            {
                key = this._config["Jwt:AdminKey"] ?? string.Empty;
            }
            else
            {
                key = this._config["Jwt:UserKey"] ?? string.Empty;
            }

            if (string.IsNullOrEmpty(token))
            {
                return new VerifyTokenRes { check = false };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var issuer = this._config["Jwt:Issuer"];
                var audience = this._config["Jwt:Audience"];

                var enc_key = Encoding.ASCII.GetBytes(key);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(enc_key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                };

                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;

                int parsedUserId = int.Parse(userId);


                return new VerifyTokenRes { check = true, user_id = parsedUserId };

            }
            catch (SecurityTokenExpiredException expEx)
            {
                return new VerifyTokenRes { check = false, exception = expEx.Message };
            }
            catch (SecurityTokenException secTEx)
            {
                return new VerifyTokenRes { check = false, exception = secTEx.Message };
            }
            catch (Exception ex)
            {
                return new VerifyTokenRes { check = false, exception = ex.Message };
            }
        }


        public string GenerateJwtToken(string user_id, bool isAdmin)
        {
            byte[] key;
            if (isAdmin)
            {
                key = Encoding.UTF8.GetBytes(this._config["Jwt:AdminKey"] ?? throw new InvalidOperationException("Admin JWT Secret is not configured in appsettings."));
            }
            else
            {
                key = Encoding.UTF8.GetBytes(this._config["Jwt:UserKey"] ?? throw new InvalidOperationException("User JWT Secret is not configured in appsettings."));
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user_id) }),
                Issuer = this._config["Jwt:Issuer"],  // Match Issuer with appsettings.json
                Audience = this._config["Jwt:Host"] ?? throw new InvalidOperationException("Host is not configured in appsettings."),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}