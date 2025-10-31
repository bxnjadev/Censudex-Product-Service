using Censudex_Product_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Censudex_Product_Service.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext() {}
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(u => u.Id);
        });

        modelBuilder.Entity<Product>()
            .Property(u => u.Id)
            .HasDefaultValue("gen_random_uuid()");
    }
    
}


