using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.DTOs;

namespace TryitterApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _singInManager;

        public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _singInManager = signInManager;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessdo em : " + DateTime.Now.ToLongDateString();
        }

        [HttpPost("register")]

        public async Task<ActionResult> RegisterUser(UserDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e=> e.Errors));
            }
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _singInManager.SingInAsync(user, false);
            return Ok();

        }
    }
}
