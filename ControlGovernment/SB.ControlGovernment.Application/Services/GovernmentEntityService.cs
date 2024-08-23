using SB.ControlGovernment.Application.DTOs;
using SB.ControlGovernment.Application.Interfaces;
using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Services
{
    public class GovernmentEntityService : IGovernmentEntityService
    {
        private readonly IGovernmentEntityRepository _repository;

        public GovernmentEntityService(IGovernmentEntityRepository repository)
        {
            _repository = repository;
        }

        public void Add(GovernmentEntityDto dto)
        {
            var entity = new GovernmentEntity
            {
                Id = 0, // El ID se asignará automáticamente
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                Name = dto.Name,
                Type = dto.Type,
                City = dto.City
            };
            _repository.Add(entity);
        }

        public void Update(GovernmentEntityDto dto)
        {
            var entity = new GovernmentEntity
            {
                Id = dto.Id,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                Name = dto.Name,
                Type = dto.Type,
                City = dto.City
            };
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public GovernmentEntityDto? GetById(int id)
        {
            var entity = _repository.GetById(id);
            return entity == null ? null : new GovernmentEntityDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Name = entity.Name,
                Type = entity.Type,
                City = entity.City
            };
        }

        public IEnumerable<GovernmentEntityDto> GetAll()
        {
            return _repository.GetAll().Select(e => new GovernmentEntityDto
            {
                Id = e.Id,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt,
                Name = e.Name,
                Type = e.Type,
                City = e.City
            }).ToList();
        }
    }
}
