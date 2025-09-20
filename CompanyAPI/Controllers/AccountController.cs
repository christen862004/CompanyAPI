using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager ,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        [HttpPost("register")]//api/account/register   (post)
        public async Task<IActionResult> register(RegisterUserDTO userFromReq)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = userFromReq.UserName,
                    Email = userFromReq.Email
                };
                IdentityResult result= await  userManager.CreateAsync(user,userFromReq.Password);
                if(result.Succeeded)
                {
                    //create cookie
                    return Ok("User Added Success");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("LoginError", item.Description);
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost("login")]//api/account/login  (post)
        public async Task<IActionResult> Login(LoginUserDto userFromReq)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userFromB=await  userManager.FindByNameAsync(userFromReq.UserName);
                if(userFromB != null)
                {
                    bool found=await userManager.CheckPasswordAsync(userFromB,userFromReq.Password);
                    if (found)
                    {
                        //create token  hgjg.jhkj.jhgh
                        //get userRoles

                        var UserRoles=await userManager.GetRolesAsync(userFromB);

                        string jti = Guid.NewGuid().ToString();
                        List<Claim> tokenClaims= new List<Claim>();
                        tokenClaims.Add(new Claim(ClaimTypes.Name, userFromB.UserName));
                        tokenClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromB.Id));
                        tokenClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, jti));
                        if (UserRoles != null)
                        {
                            foreach (var role in UserRoles)
                            {
                                tokenClaims.Add(new Claim(ClaimTypes.Role, role));
                            }
                        }
                        //---------------------------------------------------
                        string SecritKey = configuration["JWT:Key"];
                        SymmetricSecurityKey key = 
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecritKey));

                        SigningCredentials signingCredentials =
                            new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: configuration["JWT:Iss"],
                            audience: configuration["JWT:Aud"],
                            claims: tokenClaims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signingCredentials);
                        //encoding

                        return Ok(
                            new {
                                expiration=DateTime.Now.AddHours(1),
                                token=new JwtSecurityTokenHandler().WriteToken(mytoken)
                            }                        
                            );
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return BadRequest(ModelState);
        }
    }
}
