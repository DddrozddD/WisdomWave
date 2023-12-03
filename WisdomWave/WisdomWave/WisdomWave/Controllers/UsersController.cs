using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using WisdomWave.Models;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<WwUser> _userManager;

    public UsersController(UserManager<WwUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return new JsonResult(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] WwUser user)
    {
        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        return BadRequest(result.Errors);
    }

    [HttpPut("{token}")]
    public async Task<IActionResult> UpdateUser(string token, [FromBody] PushUserView user)
    {
        var claims = JwtHandler.DecodeJwtToken(token);

        WwUser thisUser = await _userManager.FindByIdAsync(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        string id = thisUser.Id;
        
        thisUser.Name = user.Name;
        thisUser.Telephone = user.Telephone;
        thisUser.Surname = user.Surname;
        thisUser.About = user.About;
        thisUser.Telephone = user.Telephone;

        var result = await _userManager.UpdateAsync(thisUser);

        if (result.Succeeded)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUsersByName([FromQuery] string userName)
    {
        var users = await _userManager.Users.Where(u => u.UserName.Contains(userName)).ToListAsync();

        if (users == null || users.Count == 0)
        {
            return NotFound();
        }

        return new JsonResult(users);
    }

}
