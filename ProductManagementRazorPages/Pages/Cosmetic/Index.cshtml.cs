using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using ProductManagementRazorPages.Service.Interface;

namespace ProductManagementRazorPages.Pages.Cosmetic
{
    public class IndexModel : PageModel
    {
        private readonly ICosmeticService _cosmeticService;
        public string? SearchTerm { get; set; }

        public IndexModel(ICosmeticService cosmeticService)
        {
            _cosmeticService = cosmeticService;
        }

        public List<CosmeticInformation> CosmeticInformation { get; set; } = new();
        public bool HasPermission { get; set; } = true;

        public string? UserRole { get; set; }
        public async Task<IActionResult> OnGetAsync(string? searchTerm)
        {
            var token = Request.Cookies["jwt_token"];
            var role = Request.Cookies["user_role"]?.ToString();
            UserRole = role;
            if (token == null || role == null) return RedirectToPage("/Login");

            if (role is "1" or "3" or "4")
            {
                SearchTerm = searchTerm;

                var all = await _cosmeticService.GetAllAsync(token);

                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    CosmeticInformation = all
                        .Where(c => c.CosmeticName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                                 || c.SkinType.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                else
                {
                    CosmeticInformation = all;
                }

                return Page();
            }
            if (role == "2")
            {
                HasPermission = false;
                return Page(); 
            }

            return Forbid();
        }
        public async Task<IActionResult> OnPostDeleteAsync(string cosmeticId)
        {
            var token = Request.Cookies["jwt_token"];
            var role = Request.Cookies["user_role"];
            if (token == null || role != "1") return Forbid();

            await _cosmeticService.DeleteAsync(cosmeticId, token);
            return RedirectToPage();
        }
    }
}
