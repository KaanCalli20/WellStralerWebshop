using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        

        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.ToTable("tblTRA_Transport");
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).HasColumnName("tTRAId");
            builder.Property(x => x.Naam).HasColumnName("tTRANaam");
            builder.Property(x => x.Eigen).HasColumnName("tTRAEigen");
        }
    }
}
