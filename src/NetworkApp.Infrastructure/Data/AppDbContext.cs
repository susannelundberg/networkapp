using Microsoft.EntityFrameworkCore;
using NetworkApp.Domain;

namespace NetworkApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Seminar> Seminars { get; set; }
}
