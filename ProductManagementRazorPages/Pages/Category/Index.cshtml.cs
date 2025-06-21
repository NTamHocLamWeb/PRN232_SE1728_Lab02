using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementRazorPages.Service.Interface;

namespace ProductManagementRazorPages.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICosmeticService _service;

        public IndexModel(ICosmeticService categoryService)
        {
            _service = categoryService;
        }

        public List<CosmeticCategory> Categories { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["jwt_token"];
            var role = Request.Cookies["user_role"];

            if (token == null || role != "1") return Forbid();

            Categories = await _service.GetAllCategoryAsync(token);
            return Page();
        }
    }
}
