using HajjProduct.Api.Authentication.Configrations;
using HajjProduct.Api.Authentication.Configrations.Models.Generic;
using HajjProduct.Domain.Models;
using HajjProduct.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HajjProduct.Api.Authentication.Services;

public class AuthServices (ApplicationDbContext context, IOptionsMonitor<JwtConfig> optionsMonitor)
{
    public async Task<TokenData> GenerateJwtTokenAsync(User user)
    {
        var key = Encoding.ASCII.GetBytes(optionsMonitor.CurrentValue.secret);
        var jwtHandler = new JwtSecurityTokenHandler();

        // select roles from database and add them to claims
        //var roles = await userManager.GetRolesAsync(user);
        //var claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToArray();

        var tokenDiscriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            }.Concat([])),  // here add the roles to claims)),
            Expires = DateTime.UtcNow.Add(optionsMonitor.CurrentValue.ExpireTimeFrame),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = jwtHandler.CreateToken(tokenDiscriptor);
        var jwtToken = jwtHandler.WriteToken(token);

        var refreshToken = new RefreshToken
        {
            CreatedAt = DateTime.UtcNow,
            Token = $"{RandomStringGenerator(25)}_{Guid.NewGuid()}",
            UserId = user.Id.ToString(),
            IsRevoked = false,
            IsUsed = false,
            JwtId = token.Id,
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };

        await context.RefreshTokens.AddAsync(refreshToken);
        context.SaveChanges();

        return new TokenData
        {
            Token = jwtToken,
            RefreshToken = refreshToken.Token
        };
    }

    private string RandomStringGenerator(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

}
