using Newtonsoft.Json;
using SB.ControlGovernment.Application.Interfaces;
using SB.ControlGovernment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ControlGovernment.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepositories<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly string _filePath;

        public BaseRepository(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, string.Empty);
            }
        }

        public IEnumerable<TEntity> LoadData()
        {
            var entities = new List<TEntity>();
            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var values = line.Split(',');
                    if (values.Length == 4) 
                    {
                        if (int.TryParse(values[0], out var id) &&
                            DateTime.TryParse(values[1], out var createdAt) &&
                            (DateTime.TryParse(values[2], out var updatedAt) || string.IsNullOrEmpty(values[2])))
                        {
                            var entity = Activator.CreateInstance<TEntity>();
                            entity.Id = id;
                            entity.CreatedAt = createdAt;
                            entity.UpdatedAt = string.IsNullOrEmpty(values[2]) ? (DateTime?)null : updatedAt;
                            
                            entities.Add(entity);
                        }
                    }
                }
            }
            return entities;
        }

        public void SaveData(IEnumerable<TEntity> entities)
        {
            var lines = entities.Select(e => $"{e.Id},{e.CreatedAt:yyyy-MM-dd},{e.UpdatedAt:yyyy-MM-dd}");
            File.WriteAllLines(_filePath, lines);
        }

        public virtual void Add(TEntity entity)
        {
            var entities = LoadData().ToList();
            entities.Add(entity);
            SaveData(entities);
        }

        public virtual void Update(TEntity entity, Func<TEntity, bool> predicate)
        {
            var entities = LoadData().ToList();
            var index = entities.FindIndex(new Predicate<TEntity>(predicate));
            if (index != -1)
            {
                entities[index] = entity;
                SaveData(entities);
            }
        }

        public virtual void Delete(Func<TEntity, bool> predicate)
        {
            var entities = LoadData().ToList();
            var entityToRemove = entities.FirstOrDefault(predicate);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
                SaveData(entities);
            }
        }

        public virtual TEntity GetById(Func<TEntity, bool> predicate)
        {
            return LoadData().FirstOrDefault(predicate);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return LoadData();
        }
    }
}
