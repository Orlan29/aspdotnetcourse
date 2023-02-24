using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UnivAssurance.Auth;
using UnivAssurance.Auth.Models;
using UnivAssurance.Auth.Utilities;
using Response = UnivAssurance.WebAPI.Auth.Utilities.Response;

namespace UnivAssurance.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private UserManager<ApplicationUser> UserManager;
    private RoleManager<IdentityRole> RoleManager;
    private IConfiguration _Configuration;

    public AuthController(UserManager<ApplicationUser> UserManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        this.UserManager = UserManager;
        this.RoleManager = roleManager;
        _Configuration = configuration;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateUser(NewUser newUser)
    {
        var currentUser = new ApplicationUser()
        {
            Email = newUser.Email,
            UserName = newUser.UserName
        };

        var result = await UserManager.CreateAsync(currentUser, newUser.Password);
        // new Guid().ToString();
        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] Login login)
    {
        var validUser = await UserManager.FindByNameAsync(login.UserName);
        if (validUser != null)
        {
            var pV = await UserManager.CheckPasswordAsync(validUser, login.Password);
            if (pV)
            {
                var userRole = await UserManager.GetRolesAsync(validUser);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var r in userRole)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, r));
                }


                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));
                var token = new JwtSecurityToken(
                        issuer: "http://localhost:5161",
                        audience: "http://localhost:5161",
                        expires: DateTime.Now.AddHours(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
        }
        return Unauthorized();
    }

    [HttpPost]
    [Route("registeradmin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] NewUser model)
    {
        var userExists = await UserManager.FindByNameAsync(model.UserName);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        ApplicationUser user = new ApplicationUser()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.UserName
        };
        var result = await UserManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        if (!await RoleManager.RoleExistsAsync(RoleUser.Admin))
            await RoleManager.CreateAsync(new IdentityRole(RoleUser.Admin));
        if (!await RoleManager.RoleExistsAsync(RoleUser.User))
            await RoleManager.CreateAsync(new IdentityRole(RoleUser.User));

        if (await RoleManager.RoleExistsAsync(RoleUser.Admin))
        {
            await UserManager.AddToRoleAsync(user, RoleUser.Admin);
        }

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }
}