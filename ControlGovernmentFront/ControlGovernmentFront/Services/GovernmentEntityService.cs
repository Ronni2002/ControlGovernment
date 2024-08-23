using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ControlGovernmentFront.Models;
using Microsoft.AspNetCore.Http;

namespace ControlGovernmentFront.Services
{
    public class GovernmentEntityService : IGovernmentEntityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GovernmentEntityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddAuthorizationHeader()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IEnumerable<GovernmentEntity>> GetAllAsync()
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<IEnumerable<GovernmentEntity>>("https://localhost:44331/api/GovernmentEntities");
        }

        public async Task<GovernmentEntity> GetByIdAsync(int id)
        {
            AddAuthorizationHeader(); 
            return await _httpClient.GetFromJsonAsync<GovernmentEntity>($"https://localhost:44331/api/GovernmentEntities/{id}");
        }

        public async Task CreateAsync(GovernmentEntity entity)
        {
            AddAuthorizationHeader(); 
            await _httpClient.PostAsJsonAsync("https://localhost:44331/api/GovernmentEntities", entity);
        }

        public async Task UpdateAsync(GovernmentEntity entity)
        {
            AddAuthorizationHeader(); 
            await _httpClient.PutAsJsonAsync($"https://localhost:44331/api/GovernmentEntities/{entity.Id}", entity);
        }

        public async Task DeleteAsync(int id)
        {
            AddAuthorizationHeader();
            await _httpClient.DeleteAsync($"https://localhost:44331/api/GovernmentEntities/{id}");
        }
    }
}
