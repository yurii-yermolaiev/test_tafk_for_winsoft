using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.Core.Entities;
using Test.Models;
using Test.Repositories.Interfaces;
using Test.Services.Interfacess;

namespace Test.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {

        private readonly IMapper _mapper;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IJwtTokenService _jwtTokenService;

        private readonly ILogger<ApplicationUsersController> _logger;

        public ApplicationUsersController( IMapper mapper,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenService jwtTokenService,
            ILogger<ApplicationUsersController> logger)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        //Знаю что код нельзя писать в контроллере, просто делаю так чтобы побыстрее(тестовое задание, сами понимаете)

        [HttpPost("registration")]
        public async Task<ActionResult> PostUserAsync([FromBody] AuthModel model)
        {
            _logger.LogInformation($"method: {nameof(PostUserAsync)}, body : {JsonConvert.SerializeObject(model)}");
            
            var user = _mapper.Map<ApplicationUser>(model);
            user.Email = model.Email;
            user.UserName = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(true);

            if (result.Succeeded)
            {
                return Ok(new { Token = await _jwtTokenService.GetJwtTokenWithRolesAsync(user.Email, user), Id = user.Id });
            }
            else if (result.Errors.Any())
            {
                return BadRequest(result.Errors.Any());
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] AuthModel model)
        {
            _logger.LogInformation($"method: {nameof(LoginAsync)}, email : {model.Email}");

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false).ConfigureAwait(false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    return Ok(new
                    {
                        Token = await _jwtTokenService.GetJwtTokenWithRolesAsync(model.Email, user),
                        Id = user.Id
                    });
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("currentUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<ApplicationUser>> GetUserAsync()
        {
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name);

            return Ok(user);
        }      
    }
}
