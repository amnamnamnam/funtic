using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FUNTIK.Models;

namespace FUNTIK.Data;

public class ApplicationDbContext : IdentityDbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();
    }
    public DbSet<Contact> Contact { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<Recipe> Recipes { get; set; } = null!;
    //public DbSet<User> Users { get; set; } = null!;
}
