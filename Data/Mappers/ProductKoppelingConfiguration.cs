using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class ProductKoppelingConfiguration : IEntityTypeConfiguration<ProductKoppeling>
    {
        public void Configure(EntityTypeBuilder<ProductKoppeling> builder)
        {
            builder.ToTable("tblPRK_ProductKoppeling");

            builder.Property(x => x.Id).HasColumnName("TPRKId");

            builder.HasKey(t => new { t.HoofdId, t.GekoppeldProdId });
            builder.Property(x => x.HoofdId).HasColumnName("TPRKHoofdProId");
            builder.Property(x => x.GekoppeldProdId).HasColumnName("TPRKGekoppeldProId");
            builder.Property(x => x.KoppelVolgorde).HasColumnName("tPRKKoppelVolgorde");
            //builder.Property(x => x.KoppelType).HasColumnName("TPRKKoppelType");
            builder.HasOne(p => p.HoofdProduct)
                .WithMany(p=>p.productKoppelingen)
                .HasForeignKey(p=>p.HoofdId);
            builder.HasOne(p => p.GekoppeldProduct)
                .WithMany()
                .HasForeignKey(p => p.GekoppeldProdId);

            builder.HasOne(p => p.KoppelType)
                .WithMany()
                .HasForeignKey("TPRKKoppelType");

           
            
        }
    }
}
