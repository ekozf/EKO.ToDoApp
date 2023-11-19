using EKO.ToDoApp.AppLogic.Services.Contracts;
using EKO.ToDoApp.Shared.Requests.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EKO.ToDoApp.Web.Controllers;

[Route("[controller]")]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest model)
    {
        _ = await _userService.CreateNewUser(model);

        return Ok();
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Tasks");
        }


        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest model)
    {
        var user = await _userService.GetUserByEmailAndPassword(model.Email, model.Password);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, "User"),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties();

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Ok(new
        {
            user.Id,
            user.FirstName,
            user.LastName
        });
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(DeleteUserRequest model)
    {
        await _userService.DeleteUser(model.Id);

        return Ok();
    }

    [HttpGet("get-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();

        return Ok(users);
    }

    [Authorize]
    [HttpGet("get-claims")]
    public IActionResult GetClaims()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });

		return Ok(claims);
    }
}
