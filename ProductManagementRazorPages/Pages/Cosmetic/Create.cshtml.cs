using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using ProductManagementRazorPages.Service.Interface;

namespace ProductManagementRazorPages.Pages.Cosmetic
{
    public class CreateModel : PageModel
    {
        private readonly ICosmeticService _service;
        public CreateModel(ICosmeticService service) => _service = service;

        [BindProperty]
        public CosmeticInformation Cosmetic { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var token = Request.Cookies["jwt_token"];
            var categories = await _service.GetAllCategoryAsync(token);
            ViewData["Category"] = new SelectList(categories, "CategoryId", "CategoryName");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var token = Request.Cookies["jwt_token"];
            var role = Request.Cookies["user_role"]?.ToString();

            if (role == "1") // Only Admin
            {
                await _service.CreateAsync(Cosmetic, token);
                return RedirectToPage("Index");
            }

            return Forbid();
        }
    }
}
