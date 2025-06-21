using BusinessObjects;
using ProductManagementRazorPages.Service.Interface;
using ProductManagementRazorPages.Utils;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ProductManagementRazorPages.Service
{
    public class CosmeticService : ICosmeticService
    {
        private readonly HttpClient _httpClient;

        public CosmeticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CosmeticInformation>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var res = await _httpClient.GetFromJsonAsync<List<CosmeticInformation>>(BaseUrl.GetBaseINfoUrl());
            return res ?? new List<CosmeticInformation>();
        }

        public async Task<CosmeticInformation> GetByIdAsync(string id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await _httpClient.GetFromJsonAsync<CosmeticInformation>(BaseUrl.GetBaseINfoUrl() + id);
        }

        public async Task CreateAsync(CosmeticInformation dto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await _httpClient.PostAsJsonAsync(BaseUrl.GetBaseINfoUrl(), dto);
        }

        public async Task UpdateAsync(CosmeticInformation dto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await _httpClient.PutAsJsonAsync(BaseUrl.GetBaseINfoUrl() + dto.CosmeticId, dto);
        }

        public async Task DeleteAsync(string id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            await _httpClient.DeleteAsync(BaseUrl.GetBaseINfoUrl() + id);
        }

        public async Task<List<CosmeticCategory>> GetAllCategoryAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await _httpClient.GetFromJsonAsync<List<CosmeticCategory>>(BaseUrl.GetBaseUrl() + "CosmeticCategories");
            return result ?? new List<CosmeticCategory>();
        }
    }
}
