# Blog.SelfSignedJWT

This repository contains example ASP.NET Core Web API project, that generate self signed JWT token.

It exposes two endpoints:
- `/test` protected endpoint for test purpose
- `/auth/token` public endpoint for token generation purpose

To generate a JWT token send POST request to `/auth/token` endpoint with paylod:
```
{ email: "user@example.com", password: "1234" }
```

Used libraries:
- Microsoft.IdentityModel.Tokens (for token generation)
- Microsoft.AspNetCore.Authentication.JwtBearer (fro token validation)





