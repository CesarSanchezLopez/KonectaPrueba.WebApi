using KonectaPrueba.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KonectaPrueba.WebApi.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using KonectaPrueba.WebApi.Repository;

namespace KonectaPrueba.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DatabaseContext _context;

        private readonly IConfiguration _config;
        private readonly IAuthService _authService;
        private readonly IUser _userRepository;

        public TokenController(IConfiguration config, DatabaseContext context, IAuthService tokenService, IUser userRepository)
        {
            _configuration = config;
            _context = context;

            _authService = tokenService;
            _userRepository = userRepository;
            _config = config;
        }

       

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login(User userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return BadRequest("userName y password Raqueridos");
            }
            IActionResult response = Unauthorized();
            var validUser = _userRepository.GetUser(userModel);

            var fechaActual = DateTime.UtcNow;
            var validez = TimeSpan.FromHours(5);
            if (validUser != null)
            {
                var generatedToken = _authService.GenerateToken(fechaActual, userModel.UserName, validez);
                if (generatedToken != null)
                {
                    return Ok(generatedToken);
                    
                }
                //else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(string user, string password)
        {
            return await _context.UserInfos.FirstOrDefaultAsync(u => u.UserName == user && u.Password == password);
        }


    }
}
