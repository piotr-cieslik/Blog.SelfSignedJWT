namespace Blog.SelfSignedJWT.Models;

public sealed class TokenResponseDto
{
    public string AccessToken { get; set; }

    public DateTime ExpiresAt { get; set; }
}