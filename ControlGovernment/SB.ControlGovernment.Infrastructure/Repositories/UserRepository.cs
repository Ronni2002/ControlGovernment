using SB.ControlGovernment.Application.Interfaces;
using SB.ControlGovernment.Domain.Entities;

namespace SB.ControlGovernment.Infrastructure.Repositories
{
    public class UserRepository : FileRepository<User>, IUserRepository
    {
        public UserRepository(string filePath) : base(filePath)
        {
        }

        private int GetNextId()
        {
            var users = LoadData().ToList();
            return users.Any() ? users.Max(u => u.Id) + 1 : 1;
        }

        public void Add(User user)
        {
            int nextId = GetNextId();
            user.Id = nextId;
            var users = LoadData().ToList();
            users.Add(user);
            SaveData(users);
        }

        public void Update(User user)
        {
            var users = LoadData().ToList();
            var index = users.FindIndex(u => u.Id == user.Id);
            if (index != -1)
            {
                users[index] = user;
                SaveData(users);
            }
        }

        public void Delete(int id)
        {
            var users = LoadData().ToList();
            var userToRemove = users.FirstOrDefault(u => u.Id == id);
            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                SaveData(users);
            }
        }

        public User GetById(int id)
        {
            return LoadData().FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return LoadData();
        }

        public User GetByEmail(string email)
        {
            return LoadData().FirstOrDefault(u => u.Email == email);
        }
    }
}
