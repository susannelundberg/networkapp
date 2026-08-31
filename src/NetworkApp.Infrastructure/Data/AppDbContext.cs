using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetworkApp.Domain;
using NetworkApp.Domain.Entities;

namespace NetworkApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Seminar> Seminars { get; set; }
}
