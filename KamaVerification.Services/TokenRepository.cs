using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using KamaVerification.Data;
using KamaVerification.Data.Models;
using KamaVerification.Data.Dtos;

namespace KamaVerification.Services
{
    public interface ITokenRepository
    {
        TokenResponse BuildToken(Customer customer);
    }

    public class TokenRepository : ITokenRepository
    {
        private readonly ILogger<TokenRepository> _logger;
        private readonly IConfiguration _config;

        public TokenRepository(
            ILogger<TokenRepository> logger,
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
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
            var expires = Convert.ToInt32(_config[Keys.JwtExpires]);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[Keys.JwtKey]));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(expires),
                Issuer = _config[Keys.JwtIssuer],
                Audience = _config[Keys.JwtAudience],
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