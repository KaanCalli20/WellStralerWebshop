using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.ToTable("tblPROTyp_ProductType");
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Id).HasColumnName("tPROTYPID");
            builder.Property(x => x.NaamNL).HasColumnName("tPROTYPNaam");
            builder.Property(x => x.NaamFR).HasColumnName("tPROTYPNaamFR");
            builder.Property(x => x.NaamEN).HasColumnName("tPROTYPNaamEN");
            builder.Property(x => x.NaamDU).HasColumnName("tPROTYPNaamDU");

        }
    }
}
