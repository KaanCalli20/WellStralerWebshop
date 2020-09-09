using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Data.Mappers;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Product> Producten { get; set; }
        public DbSet<Klant> Klanten { get; set; }
        public DbSet<KlantLogin> KlantLogins { get; set; }
        public DbSet<OnlineBestelling> OnlineBestellingen {get;set;}
        public DbSet<OnlineBestelLijn> OnlineBestelLijnen { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ProductKoppelingConfiguration());
            builder.ApplyConfiguration(new KopTypesConfiguration());
            builder.ApplyConfiguration(new ProductTypeConfiguration());

            builder.ApplyConfiguration(new KlantConfiguration());
            builder.ApplyConfiguration(new KlantKoppelingConfiguration());
            builder.ApplyConfiguration(new KlantLoginConfiguration());

            builder.ApplyConfiguration(new OnlineBestelLijnConfiguration());
            builder.ApplyConfiguration(new OnlineBestellingConfiguration());

            builder.ApplyConfiguration(new TransportConfiguration());


        }

    }
}
