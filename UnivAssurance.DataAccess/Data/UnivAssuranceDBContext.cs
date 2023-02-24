using Microsoft.EntityFrameworkCore;
using UnivAssurance.DataAccess.Models;

namespace UnivAssurance.DataAccess.Data;

public class UnivAssuranceDBContext: DbContext
{
    public virtual DbSet<Person> Person {get; set;}
    public virtual DbSet<Product> Product {get; set;}
    public virtual DbSet<Comercial> Comercial {get; set;}
    public virtual DbSet<Subscription> Subscription {get; set;}

    public UnivAssuranceDBContext()
    {}

    public UnivAssuranceDBContext(DbContextOptions<DbContext> options): base(options)
    {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbSeting = "Server=DESKTOP-L922P58;Database=UnivAssurance;User Id=sa;Password=1234;Encrypt=false;";

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(dbSeting);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(
            entity => {
                entity.HasKey(properties => properties.PersonID);
            }  
        );

        modelBuilder.Entity<Product>(
            entity => {
                entity.HasKey(properties => properties.ProductID);
            }  
        );

        modelBuilder.Entity<Comercial>(
            entity => {
                entity.HasKey(properties => properties.ComercialID);
            }  
        );

        modelBuilder.Entity<Subscription>(
            entity => {
                entity.HasKey(properties => properties.SubscriptionID);
            }  
        );
    }
}