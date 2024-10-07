using KonectaPrueba.WebApi.Interface;
using KonectaPrueba.Models;
using KonectaPrueba.WebApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KonectaPrueba.WebApi.Services
{
   
        public class AuthService : IAuthService
        {
        private readonly IConfiguration _configuration;
        private readonly UserRepository _UserInfRepository;

        public AuthService(IConfiguration config)
        {



            _configuration = config;
        }
        public bool ValidateLogin(User userModel)
            {
            //aqui haríamos la validación, de momento simulamos validación login

            var user = GetUser(userModel);

            if (user != null)
            {
                return true;
            }
            else
            { 
            return false;
            }
            }
           
            public string GenerateToken(DateTime fechaActual, string username, TimeSpan tiempoValidez)
            {
                var fechaExpiracion = fechaActual.Add(tiempoValidez);



           
            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", username)
                        
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
                return encodedJwt;
            }
        public bool ValidateToken(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private  User GetUser(User userModel)
        {
            return  _UserInfRepository.GetUser(userModel);
        }
    }
    
}
