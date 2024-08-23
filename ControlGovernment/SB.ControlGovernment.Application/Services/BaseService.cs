using SB.ControlGovernment.Application.Interfaces;
using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Application.Services
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly IBaseRepositories<TEntity> repository;

        public BaseService(IBaseRepositories<TEntity> repository)
        {
            this.repository = repository;
        }

        public virtual void Add(TEntity entity)
        {
            repository.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            repository.Update(entity, e => e.Id == entity.Id);
        }

        public virtual void Delete(int id)
        {
            repository.Delete(e => e.Id == id);
        }

        public virtual TEntity GetById(int id)
        {
            return repository.GetById(e => e.Id == id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return repository.GetAll();
        }
    }
}
