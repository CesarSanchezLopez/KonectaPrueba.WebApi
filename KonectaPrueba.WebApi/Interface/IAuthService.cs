using KonectaPrueba.Models;

namespace KonectaPrueba.WebApi.Interface
{
    public interface IAuthService
    {
        public bool ValidateLogin(User userModel);
        string GenerateToken(DateTime fechaActual, string username, TimeSpan tiempoValidez);

        bool ValidateToken(string key, string issuer, string token);
    }
}
