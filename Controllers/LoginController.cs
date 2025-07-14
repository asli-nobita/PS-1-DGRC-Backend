using Microsoft.AspNetCore.Mvc;
using DGRC.Data; 
using DGRC.Models;

namespace DGRC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public IActionResult Login(LoginRequest request)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);

        if (user == null)
            return Unauthorized("Invalid username or password");

        return Ok("Login successful");
    }
}
