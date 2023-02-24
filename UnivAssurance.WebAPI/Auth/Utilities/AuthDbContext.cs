using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UnivAssurance.Auth;

public class AuthDbContext: IdentityDbContext<ApplicationUser>
{
    public AuthDbContext()
    {}

     public AuthDbContext(DbContextOptions<IdentityDbContext> options): base(options)
    {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dbSeting = "Server=DESKTOP-L922P58;Database=UnivAssurance;User Id=sa;Password=1234;Encrypt=false;";

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(dbSeting);
        }
    }

}