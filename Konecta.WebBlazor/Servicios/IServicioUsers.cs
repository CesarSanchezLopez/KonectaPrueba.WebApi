using KonectaPrueba.Models;

namespace Konecta.WebBlazor.Servicios
{
    public interface IServicioUsers
    {

        Task<IEnumerable<User>> GetUsers();
    }
}
