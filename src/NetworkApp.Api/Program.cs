using NetworkApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NetworkApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using NetworkApp.Application;
using NetworkApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }

    if (!await roleManager.RoleExistsAsync("Manager"))
    {
        await roleManager.CreateAsync(new IdentityRole("Manager"));
    }

    var result = await userManager.FindByEmailAsync("admin@admin123.se");

    if (result is null)
    {
       var user = new User()
       {
           FirstName = "Förnamn",
           LastName = "Efternamn",
           Email = "admin@admin123.se",
           UserName = "admin@admin123.se"
       };

       var admin = await userManager.CreateAsync(user, "Admin123!");

       if (!admin.Succeeded)
        {
            Console.WriteLine("Lösenordet behöver ha stor och liten bokstav, en siffra och ett specialtecken");
        }

       await userManager.AddToRoleAsync(user, "Admin");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
