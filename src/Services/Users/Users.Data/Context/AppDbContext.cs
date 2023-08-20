using Microsoft.EntityFrameworkCore;
using Users.Data.Models;

namespace Users.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserDbModel> Users { get; set; }
}