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
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.HoofdProduct)
                .WithMany()
                .HasForeignKey("TPRKHoofdProId");
            builder.HasOne(p => p.GekoppeldProduct)
                .WithMany(t => t.productKoppelingen)
                .HasForeignKey("TPRKGekoppeldProId");
            /*builder.HasOne(p => p.KoppelType)
                .WithOne()
                .HasForeignKey("TPRKKoppelType");*/
            
        }
    }
}
