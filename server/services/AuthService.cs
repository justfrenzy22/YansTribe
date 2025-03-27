using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using server.responses;

namespace server.services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config) => this._config = config;

        public VerifyTokenRes VerifyTokenAsync(string token)
        {

            if (string.IsNullOrEmpty(token))
            {
                return new VerifyTokenRes { check = false };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var key = this._config["Jwt:Key"] ?? "";
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
            catch
            {
                // Token validation failed
                return new VerifyTokenRes { check = false };
            }
        }


        public string GenerateJwtToken(string user_id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(this._config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Secret is not configured in appsettings."));

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