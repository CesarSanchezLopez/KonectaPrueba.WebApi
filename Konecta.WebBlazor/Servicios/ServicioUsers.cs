using KonectaPrueba.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace Konecta.WebBlazor.Servicios
{
    public class ServicioUsers : IServicioUsers
    {
        private readonly HttpClient httpClient;    
        ServicioUsers(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await httpClient.GetFromJsonAsync<User[]>("api/User");

        }

    }
}
