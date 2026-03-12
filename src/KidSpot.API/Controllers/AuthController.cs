using KidSpot.Application.Common;
using KidSpot.Application.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace KidSpot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthenticateWithGoogleHandler _authenticateHandler;

    public AuthController(AuthenticateWithGoogleHandler authenticateHandler)
    {
        _authenticateHandler = authenticateHandler;
    }

    [HttpPost("google")]
    public async Task<IActionResult> Google(
        [FromBody] GoogleAuthRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authenticateHandler.HandleAsync(
            new AuthenticateWithGoogleCommand(request.IdToken),
            cancellationToken);

        if (result.Error is not null)
            return Unauthorized(result);

        // Set JWT as httpOnly cookie
        Response.Cookies.Append("token", result.Data!.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // true in production
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        return Ok(result);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");
        return Ok(ApiResponse<object>.Success(new { message = "Logged out successfully." }));
    }
}

public record GoogleAuthRequest(string IdToken);
