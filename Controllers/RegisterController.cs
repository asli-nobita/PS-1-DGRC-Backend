using Microsoft.AspNetCore.Mvc;
using DGRC.Data;
using DGRC.Models; 


namespace UserAuthAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        if (_context.Users.Any(u => u.Username == request.Username))
            return BadRequest("Username already exists");

        if (_context.Users.Any(u => u.Email == request.Email))
            return BadRequest("Email already registered");

        var user = new User
        {
            Username = request.Username,
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password,
            PhoneNumber = request.MobileNumber
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("User registered successfully.");
    }
}
