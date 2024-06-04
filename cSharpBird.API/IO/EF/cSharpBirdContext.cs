using Microsoft.EntityFrameworkCore;

namespace cSharpBird.API;

public class cSharpBirdContext : DbContext
{
    public DbSet<User> Users {get; set;}
    public DbSet<Checklist> Checklists {get; set;}
    public DbSet<Bird> Birds {get; set;}

    public cSharpBirdContext() {}
    public cSharpBirdContext(DbContextOptions options) : base (options) {}

    protected override void ModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bird>().UseTpcMappingStrategy().ToTable(Birds);
        modelBuilder.Entity<Checklist>().UseTpcMappingStrategy().ToTable(Checklists);
        modelBuilder.Entity<User>().UseTpcMappingStrategy().ToTable(Users);
    }
}