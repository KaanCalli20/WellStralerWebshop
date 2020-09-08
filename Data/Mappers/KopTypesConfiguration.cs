using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class KopTypesConfiguration : IEntityTypeConfiguration<KopTypes>
    {
        public void Configure(EntityTypeBuilder<KopTypes> builder)
        {
            builder.ToTable("tblKOP_Types");
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).HasColumnName("TKOPId");
            builder.Property(x => x.Naam).HasColumnName("TKOPNaam");

        }
    }
}
