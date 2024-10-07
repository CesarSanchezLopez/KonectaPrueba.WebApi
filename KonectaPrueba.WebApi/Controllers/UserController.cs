using KonectaPrueba.WebApi.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KonectaPrueba.Models;
using Microsoft.AspNetCore.Authorization;

namespace KonectaPrueba.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _User;

        public UserController(IUser IUser)
        {
            _User = IUser;
        }

        // GET: api/employee>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await Task.FromResult(_User.GetUserDetails());
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var users = await Task.FromResult(_User.GetUserDetails(id));
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        // POST api/employee
        [HttpPost]
        public async Task<ActionResult<User>> Post(User user)
        {
            _User.AddUser(user);
            return await Task.FromResult(user);
        }

        // PUT api/employee/5
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            try
            {
                _User.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(user);
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            var user = _User.DeleteUser(id);
            return await Task.FromResult(user);
        }

        private bool UserExists(int id)
        {
            return _User.CheckUser(id);
        }
    }
}
