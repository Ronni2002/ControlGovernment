using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Interfaces
{
    public interface IBaseRepositories<TEntity> where TEntity : IBaseEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity, Func<TEntity, bool> predicate);
        void Delete(Func<TEntity, bool> predicate);
        TEntity GetById(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> GetAll();
    }
}
