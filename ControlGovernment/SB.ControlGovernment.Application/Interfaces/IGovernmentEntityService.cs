using SB.ControlGovernment.Application.DTOs;
using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Interfaces
{
    public interface IGovernmentEntityService
    {
        void Add(GovernmentEntityDto dto);
        void Update(GovernmentEntityDto dto);
        void Delete(int id);
        GovernmentEntityDto? GetById(int id);
        IEnumerable<GovernmentEntityDto> GetAll();
    }
}
