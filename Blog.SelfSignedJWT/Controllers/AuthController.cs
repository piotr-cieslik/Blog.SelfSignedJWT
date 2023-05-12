using Blog.SelfSignedJWT.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace Blog.SelfSignedJWT.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public AuthController(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    [AllowAnonymous]
    [HttpPost("token")]
    public IActionResult Token([FromBody] TokenRequestDto dto)
    {
        // Check if user is valid here.
        // Please do it in more secure way :)
        const string expectedEmail = "user@example.com";
        const string expectedPassword = "1234";

        var authenticated =
            dto.Email== expectedEmail &&
            dto.Password == expectedPassword;

        if (!authenticated)
        {
            return BadRequest("Invalid email or password");
        }
        
        // Assume our user has id: 1.
        const int userId = 1;
        
        // Read options from configuration.
        var options = _jwtOptions.Value;

        // Take snapshot of current time and calculate when token expires.
        var now = DateTime.UtcNow;
        var expiresAt = now.AddMinutes(15);

        // Create SigningCredentials using symmetric key from settings.
        var signingCredentials =
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(options.Key)),
                SecurityAlgorithms.HmacSha256);

        // Optionally define list of extra claims for token.
        var claims =
            new List<Claim>()
            {
                new (JwtRegisteredClaimNames.Sub, userId.ToString()),
                new (ClaimTypes.Email, dto.Email),
            };

        // Create a token model.
        var token =
            new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now,
                expires: expiresAt,
                signingCredentials: signingCredentials);

        // Generate an access token.
        var accessToken =
            new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new TokenResponseDto
        {
            AccessToken = accessToken,
            ExpiresAt = expiresAt,
        });
    }
}