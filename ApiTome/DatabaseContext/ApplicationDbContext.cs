using ApiBasicsService.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBasicsService.DatabaseContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) {
    }

    public ApplicationDbContext(){}

    public virtual DbSet<City> Cities {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<City>().HasData(
            new City(){CityID = Guid.Parse("BF160CFD-E693-4C6A-9417-037B4501EC9B"), CityName = "New York"},
            new City(){CityID = Guid.Parse("858462EF-5587-8DB3-39293869"), CityName = "Jaipur"}
        );
    }

}
