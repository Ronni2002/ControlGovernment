using SB.ControlGovernment.Application.Interfaces;
using SB.ControlGovernment.Domain.Entities;

namespace SB.ControlGovernment.Infrastructure.Repositories
{
    public class GovernmentEntityRepository : FileRepository<GovernmentEntity>, IGovernmentEntityRepository
    {

        public GovernmentEntityRepository(string filePath) : base(filePath)
        {
        }

        private int GetNextId()
        {
            var entities = LoadData().ToList();
            return entities.Any() ? entities.Max(e => e.Id) + 1 : 1;
        }

        public void Add(GovernmentEntity entity)
        {
            int nextId = GetNextId();
            entity.Id = nextId;
            var entities = LoadData().ToList();
            entities.Add(entity);
            SaveData(entities);
        }

        public void Update(GovernmentEntity entity)
        {
            var entities = LoadData().ToList();
            var index = entities.FindIndex(e => e.Id == entity.Id);
            if (index != -1)
            {
                entities[index] = entity;
                SaveData(entities);
            }
        }

        public void Delete(int id)
        {
            var entities = LoadData().ToList();
            var entityToRemove = entities.FirstOrDefault(e => e.Id == id);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
                SaveData(entities);
            }
        }

        public GovernmentEntity GetById(int id)
        {
            return LoadData().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<GovernmentEntity> GetAll()
        {
            return LoadData();
        }
    }
}
