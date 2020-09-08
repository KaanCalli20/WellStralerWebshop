using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Product> Producten { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<OnlineBestelling> OnlineBestellingen {get;set;}
        public DbSet<OnlineBestelLijn> OnlineBestelLijnen { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new SessieConfiguration());
            
        }

    }
}
