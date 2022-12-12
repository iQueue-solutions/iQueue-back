using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        var key = Convert.FromBase64String(secret);

        var claimsIdentity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        });

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = _configuration.GetSection("AccessToken:Issuer").Value,
            Audience = _configuration.GetSection("AccessToken:Audience").Value,
            Expires = DateTime.Now.AddMinutes(15),
            SigningCredentials = signingCredentials,

        };
        
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return await Task.Run(() => tokenHandler.WriteToken(securityToken));
    }
}