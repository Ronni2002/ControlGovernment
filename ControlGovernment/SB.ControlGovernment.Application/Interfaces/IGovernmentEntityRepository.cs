using SB.ControlGovernment.Application.DTOs;
using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Interfaces
{
    public interface IGovernmentEntityRepository : IFileRepository<GovernmentEntity>
    {
        void Add(GovernmentEntity dto);
        void Update(GovernmentEntity dto);
        void Delete(int id);
        GovernmentEntity GetById(int id);
        IEnumerable<GovernmentEntity> GetAll();
    }
}
