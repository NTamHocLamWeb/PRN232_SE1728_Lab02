using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;
using ProductManagementRazorPages.DTO;
using ProductManagementRazorPages.Utils;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class LoginModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public LoginModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public LoginRequest Login { get; set; } = new();

    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var json = JsonSerializer.Serialize(Login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine(BaseUrl.GetBaseUrl() + "SystemAccounts/Login");
        var response = await client.PostAsync(BaseUrl.GetBaseUrl() + "SystemAccounts/Login", content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
            var result = JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Response.Cookies.Append("jwt_token", result!.Token);
            Response.Cookies.Append("user_role", result.Role);
            Response.Cookies.Append("account_id", result.AccountId);

            return RedirectToPage("/Cosmetic/Index");
        }
        else
        {
            ErrorMessage = "Invalid login attempt.";
            return Page();
        }
    }
}