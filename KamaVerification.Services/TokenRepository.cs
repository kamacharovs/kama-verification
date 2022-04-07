using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using KamaVerification.Data;
using KamaVerification.Data.Models;
using KamaVerification.Data.Dtos;
using KamaVerification.Data.Options;

namespace KamaVerification.Services
{
    public interface ITokenRepository
    {
        TokenResponse BuildToken(Customer customer);
    }

    public class TokenRepository : ITokenRepository
    {
        private readonly ILogger<TokenRepository> _logger;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public TokenRepository(
            ILogger<TokenRepository> logger,
            IOptions<JwtOptions> jwtOptions)
        {
            _logger = logger;
            _jwtOptions = jwtOptions;
        }

        private IEnumerable<Claim> BuildClaims(Customer customer)
        {
            return new[]
            {
                new Claim("customer_name", customer.Name.ToLowerInvariant()),
                new Claim("customer_public_key", customer.PublicKey.ToString())
            };
        }

        public TokenResponse BuildToken(Customer customer)
        {
            var claims = BuildClaims(customer);
            var expires = Convert.ToInt32(_jwtOptions.Value.Expires);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(expires),
                Issuer = _jwtOptions.Value.Issuer,
                Audience = _jwtOptions.Value.Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResponse
            {
                ExpiresIn = expires,
                AccessToken = tokenHandler.WriteToken(token)
            };
        }
    }
}