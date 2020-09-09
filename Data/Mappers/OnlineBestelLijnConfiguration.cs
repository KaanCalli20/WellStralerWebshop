using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class OnlineBestelLijnConfiguration : IEntityTypeConfiguration<OnlineBestelLijn>
    {

        public void Configure(EntityTypeBuilder<OnlineBestelLijn> builder)
        {
            builder.ToTable("tblOBESL_OnlineBestelLijnen");
            builder.Property(x => x.Id).HasColumnName("tOBESLId");
            builder.Property(x => x.BestellingId).HasColumnName("tOBESLBesId");
            builder.Property(x => x.ProductId).HasColumnName("tOBESLPROId");
            builder.Property(x => x.Aantal).HasColumnName("tOBESLAantal");
            builder.Property(x => x.Prijs).HasColumnName("tOBESLPrijs");
            builder.Property(x => x.BtwPerc).HasColumnName("tOBESLBtwPerc");
            builder.Property(x => x.KlantId).HasColumnName("tOBESLKlaId");
            builder.Property(x => x.DatumInbreng).HasColumnName("tOBESLDatumInbreng");
            builder.Property(x => x.HoofdProdBestelLijnId)
                .HasColumnName("tOBESLHoofdProdBeslId");
            builder.Property(x => x.KlantLoginId).HasColumnName("tOBESLKllId");
            builder.Property(x => x.HoofdProdBestelLijnId).HasColumnName("tOBESLHoofdProdBeslId");

            builder.HasOne(p => p.KlantLogin)
                .WithMany()
                .HasForeignKey(p => p.KlantLoginId);

            builder.HasOne(p => p.Klant)
                .WithMany()
                .HasForeignKey(p => p.KlantId);

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            

        }
    }
}
