using BudgetOnline.Application.Abstractions;
using BudgetOnline.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetOnline.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        var foodId = Guid.NewGuid();
        var rentId = Guid.NewGuid();

        modelBuilder.Entity<Category>().HasData(
            new Category(foodId, "Food", 100),
            new Category(rentId, "Rent", 500)
        );
    }
}
