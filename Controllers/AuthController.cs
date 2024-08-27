using Microsoft.AspNetCore.Mvc;
using TE_trsprt_remake.DTOs;
using TE_trsprt_remake.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IEmailService _emailservice;

    public AuthController(IAuthService authService, IEmailService emailservice)
    {
        _authService = authService;
        _emailservice = emailservice;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO model)
    {
        var token = await _authService.LoginAsync(model);
        if (token == null)
            return Unauthorized();

        return Ok(new { Token = token  });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO model)
    {
        var token = await _authService.RegisterAsync(model);
        if (token == null)
            return BadRequest("User already exists");

        return Ok(new { Token = token });
    }

    [HttpPost("forgotpassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] PasswordResetRequestDTO request)
    {
        

        var result = await _authService.GenerateAndSendResetPasswordRequestAsync(request.Email);
        if (!result)
            return NotFound("User with this email does not exist.");

        return Ok("Password reset request has been sent to the supervisor.");
    }

    [HttpGet("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromQuery] string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return BadRequest("Invalid token.");

        var result = await _authService.ResetPasswordBySupervisorAsync(token);
        if (!result)
            return BadRequest("Invalid token or password reset failed.");

        return Ok("Password has been reset and sent to the user.");
    }




}
