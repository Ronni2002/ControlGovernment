using SB.ControlGovernment.Application.DTOs;
using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Interfaces
{
    public interface IUserService
    {
        void Add(UserDto dto);
        void Update(UserDto dto);
        void Delete(int id);
        UserDto? GetById(int id);
        IEnumerable<UserDto> GetAll();
        Task<User> AuthenticateAsync(string email, string password);
        void Register(UserDto userDto);
    }
}
