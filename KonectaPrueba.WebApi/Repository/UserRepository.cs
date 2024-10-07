using KonectaPrueba.WebApi.Interface;
using KonectaPrueba.Models;
using Microsoft.EntityFrameworkCore;

namespace KonectaPrueba.WebApi.Repository
{
    public class UserRepository : IUser
    {
        readonly DatabaseContext _dbContext = new();
        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User GetUser(User userModel)
        {
            return _dbContext.UserInfos.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()
                && x.Password == userModel.Password).FirstOrDefault();
        }

        public List<User> GetUserDetails()
        {
            try
            {
                return _dbContext.UserInfos.ToList();
            }
            catch
            {
                throw;
            }
        }

        public User GetUserDetails(int id)
        {
            try
            {
                User? user = _dbContext.UserInfos.Find(id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddUser(User user)
        {
            try
            {
                _dbContext.UserInfos.Add(user);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public User DeleteUser(int id)
        {
            try
            {
                User? user = _dbContext.UserInfos.Find(id);

                if (user != null)
                {
                    _dbContext.UserInfos.Remove(user);
                    _dbContext.SaveChanges();
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
        public bool CheckUser(int id)
        {
            return _dbContext.UserInfos.Any(e => e.UserId == id);
        }
    }
}
