using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TryitterApi.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TryitterApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AutorizaController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AutorizaController(UserManager<IdentityUser> userManager,
         SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        
        // public AutorizaController(){}

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AutorizaController :: Acessdo em : " + DateTime.Now.ToLongDateString();
        }

        /// <summary>
        /// Registro de um novo usuário no banco de dados para acesso à API
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Criar um objeto do tipo json com as respectivas chaves/valor
        ///   chave "email" , colocar um email válido;  
        ///   chave "password", inserir um valor com o seguinte formato: 
        ///    * uma senha alfanúmerica; 
        ///    * com pelo menos 1 caracter especial;
        ///    * com no mínimo uma letra maiuscula;
        ///   chave "confirmPassword", repetir as informações inseridas em "password"
        ///
        ///   Exemplo:
        ///
        ///     Autoriza /Login
        ///     {
        ///        "email": "helena@email.com",
        ///        "password": "String1@",
        ///        "confirmPassword": "String1@"
        ///     }
        ///
        /// </remarks>
        [HttpPost("Register")]

        public async Task<ActionResult> RegisterUser(UserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
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
            await _signInManager.SignInAsync(user, false);
            return Ok(GeraToken(model));

        }
        /// <summary>
        /// Login de um usuário cadastrado no banco de dados para acesso a API
        /// </summary>
              /// <returns> Retorna um novo token criado </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Autoriza /Login
        ///     Copiar json com informações de usuário e colar para login na API
        ///
        ///     {
        ///        "email": "helena@email.com",
        ///        "password": "String1@",
        ///        "confirmPassword": "String1@"
        ///     }
        ///
        /// OBS: caso retorne "Login inválido" é necessário fazer o registro do usuário
        /// </remarks>
        [HttpPost("Login")]

        public async Task<ActionResult> Login(UserDTO userInfo)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState.Values.SelectMany(e=> e.Errors));
            }
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email, 
                userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                return Ok(GeraToken(userInfo));
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido...");
                return BadRequest(ModelState);
            }
        }

        [NonAction]
        public UserToken GeraToken(UserDTO userInfo)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim("meuClain", "cripto"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            // gera uma chave com base em um algoritimo simetrico
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
           
            // gera uma ass digital do token usando a chave privada e o algoritimo Hmac.
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            //configura o tempo de expiração do toquem com base na chave no appsettings
            var expConfig = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expConfig));

            //classe que representa um token JWT e gera token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credenciais);

            return new UserToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };   
            

        }


    }
}

