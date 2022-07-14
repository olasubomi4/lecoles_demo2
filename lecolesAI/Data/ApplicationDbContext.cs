using System;

using lecolesAI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace lecolesAI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<courses> courses { get; set; }
        
    }

    public class YourDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Data Source=127.0.0.1;Initial Catalog=lecoles;Persist Security Info=False;User ID=sa;Password=MyPass@word;MultipleActiveResultSets=False");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

}



