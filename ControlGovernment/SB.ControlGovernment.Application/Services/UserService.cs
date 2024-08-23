using SB.ControlGovernment.Application.DTOs;
using SB.ControlGovernment.Application.Helpers;
using SB.ControlGovernment.Application.Interfaces;
using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public void Add(UserDto dto)
        {
            var (hash, salt) = PasswordHelper.CreatePasswordHash(dto.Password);
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = dto.Role,
                CreatedAt = DateTime.UtcNow
            };
            _userRepository.Add(user);
        }

        public void Update(UserDto dto)
        {
            var user = new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                PasswordSalt = dto.PasswordSalt, 
                Role = dto.Role,
                UpdatedAt = DateTime.UtcNow
            };
            _userRepository.Update(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public UserDto? GetById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _userRepository.GetAll().Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            });
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
            if (user == null || !PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user;
        }

        public void Register(UserDto userDto)
        {
            var (hash, salt) = PasswordHelper.CreatePasswordHash(userDto.Password);
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = userDto.Role
            };
            _userRepository.Add(user);
        }
    }
}
