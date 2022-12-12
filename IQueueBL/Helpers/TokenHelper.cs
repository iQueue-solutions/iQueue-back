using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IQueueBL.Helpers;

public class TokenHelper
{
    private static IConfiguration _configuration;

    public TokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static async Task<string> GenerateAccessToken(Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var secret = _configuration.GetSection("AccessToken:Secret").Value;
        var key = Encoding.UTF8.GetBytes(secret);

        var claimsIdentity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        });

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = _configuration.GetSection("AccessToken:Issuer").Value,
            Expires = DateTime.Now.AddMonths(2),
            SigningCredentials = signingCredentials,
        };
        
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return await Task.Run(() => tokenHandler.WriteToken(securityToken));
    }
}