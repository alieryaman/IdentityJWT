using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityJWT.Data;
using IdentityJWT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace IdentityJWT.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }



        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {


            return new string[] { "value1", "value2" };
           
        }






        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user,model.Password))
            {

                var userRoles = await userManager.GetRolesAsync(user);

                

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

                };

                //kulnıcını rolleri claime kelneyior
                foreach (var role in userRoles)
                {
                    claims.Add( new Claim(ClaimTypes.Role,role));
                }

                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AspNetCoreDersimiii"));

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:44373/",
                    audience: "https://localhost:44373/",
                    claims:claims,
                  //  expires:DateTime.Now.AddHours(1),
                    expires: DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).AddHours(1),
                    signingCredentials:new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    
                    
                    );
            


            return Ok(new
            {

                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration=token.ValidTo,
                message="Giriş Başarılı"


            });
            }
            else
            {


                return BadRequest(new {

                    message="Login başarısızı"
                });


            }
        }


    }
}