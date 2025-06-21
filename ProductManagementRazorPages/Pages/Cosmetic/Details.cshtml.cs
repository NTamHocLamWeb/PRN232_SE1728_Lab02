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
    public class DetailsModel : PageModel
    {
        private readonly ICosmeticService _service;
        public DetailsModel(ICosmeticService service) => _service = service;

        public CosmeticInformation Cosmetic { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var token = Request.Cookies["jwt_token"];
            Cosmetic = await _service.GetByIdAsync(id, token);
            return Page();
        }
    }
}
