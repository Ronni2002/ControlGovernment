using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Interfaces
{
    public interface IUserRepository : IFileRepository<User>
    {
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        User GetById(int id);
        IEnumerable<User> GetAll();
        User GetByEmail(string email);
    }
}
