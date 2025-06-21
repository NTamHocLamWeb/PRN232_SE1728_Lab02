using BusinessObjects;

namespace ProductManagementRazorPages.Service.Interface
{
    public interface ICosmeticService
    {
        Task<List<CosmeticInformation>> GetAllAsync(string token);
        Task<List<CosmeticCategory>> GetAllCategoryAsync(string token);

        Task<CosmeticInformation> GetByIdAsync(string id, string token);
        Task CreateAsync(CosmeticInformation dto, string token);
        Task UpdateAsync(CosmeticInformation dto, string token);
        Task DeleteAsync(string id, string token);
    }
}
