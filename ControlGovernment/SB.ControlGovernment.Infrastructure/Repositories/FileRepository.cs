using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using SB.ControlGovernment.Application.Interfaces;

namespace SB.ControlGovernment.Infrastructure.Repositories
{
    public class FileRepository<TEntity> where TEntity : class
    {
        protected readonly string _filePath;

        public FileRepository(string filePath)
        {
            _filePath = filePath;
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public IEnumerable<TEntity> LoadData()
        {
            var jsonData = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return new List<TEntity>();
            }

            var entities = JsonConvert.DeserializeObject<List<TEntity>>(jsonData);
            return entities ?? new List<TEntity>();
        }

        public void SaveData(IEnumerable<TEntity> entities)
        {
            var jsonData = JsonConvert.SerializeObject(entities, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
