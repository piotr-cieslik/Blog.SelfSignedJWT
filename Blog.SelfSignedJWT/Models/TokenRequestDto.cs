namespace Blog.SelfSignedJWT.Models;

public sealed class TokenRequestDto
{
    public string Email { get; set; }

    public string Password { get; set; }
}