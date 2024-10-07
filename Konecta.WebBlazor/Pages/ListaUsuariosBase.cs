using System.ComponentModel;
using KonectaPrueba.Models;
using Microsoft.AspNetCore.Components;

namespace Konecta.WebBlazor.Pages
{
  
    public class ListaUsuariosBase : ComponentBase
    {
        [Inject]
        public IEnumerable<User> Users{ get; set; }


    }
}
