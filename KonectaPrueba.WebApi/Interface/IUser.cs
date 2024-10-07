using KonectaPrueba.Models;

namespace KonectaPrueba.WebApi.Interface
{
    public interface IUser
    {
        User GetUser(User userMode);

        public List<User> GetUserDetails();
        public User GetUserDetails(int id);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public User DeleteUser(int id);
        public bool CheckUser(int id);
    }
}
