using ProductManagementRazorPages.Service.Interface;
using ProductManagementRazorPages.Service;
namespace ProductManagementRazorPages
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddRazorPages();
            builder.Services.AddHttpClient<ICosmeticService, CosmeticService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7298");
            });
            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapRazorPages();
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Login");
                return Task.CompletedTask;
            });


            app.Run();
		}
	}
}
