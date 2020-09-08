using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class KlantKoppelingConfiguration : IEntityTypeConfiguration<KlantKoppeling>
    {
        public void Configure(EntityTypeBuilder<KlantKoppeling> builder)
        {
            builder.ToTable("tblKLK_KlantKoppeling");
            builder.HasKey(t => new { t.HoofdKlantId, t.GekoppeldKlantId });
            
            builder.Property(x => x.HoofdKlantId).HasColumnName("tKLKHoofdKLAId");
            builder.Property(x => x.GekoppeldKlantId).HasColumnName("tKLKGekoppeldKLAId");

            builder.HasOne(p => p.HoofdKlant)
                .WithMany(p => p.KlantKoppelingen)
                .HasForeignKey(p => p.HoofdKlantId);
            
            builder.HasOne(p => p.GekoppeldKlant)
                .WithMany()
                .HasForeignKey(p => p.GekoppeldKlantId);

            
        }
    }
}
