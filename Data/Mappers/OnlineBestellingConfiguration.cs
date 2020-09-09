using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class OnlineBestellingConfiguration :  IEntityTypeConfiguration<OnlineBestelling>
    {
        
        public void Configure(EntityTypeBuilder<OnlineBestelling> builder)
        {
            builder.ToTable("tblOBES_OnlineBestelling");
            builder.Property(x => x.Id).HasColumnName("tOBESId");
            builder.Property(x => x.Datum).HasColumnName("tOBESDatum");
            builder.Property(x => x.KlantId).HasColumnName("tOBESKLAId");
            builder.Property(x => x.LeverKlantId).HasColumnName("tOBESLEVKLAId");
            builder.Property(x => x.Referentie).HasColumnName("tOBESReferentie");
            builder.Property(x => x.Opmerking).HasColumnName("tOBESOpmerking");
            builder.Property(x => x.TransportId).HasColumnName("tOBESTransport");
            builder.Property(x => x.DatumInBreng).HasColumnName("tOBESDatumInbreng");
            builder.Property(p => p.KlantLoginId).HasColumnName("tOBESKllId");

            builder.HasMany(t => t.OnlineBesltelLijnen)
                .WithOne().IsRequired()
                .HasForeignKey(t => t.BestellingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Klant)
                .WithMany()
                .HasForeignKey(p => p.KlantId);

            builder.HasOne(p => p.LeverKlant)
                .WithMany()
                .HasForeignKey(p => p.LeverKlantId);

            builder.HasOne(p => p.Transport)
                .WithMany()
                .HasForeignKey(p => p.TransportId);

            builder.HasOne(p => p.KlantLogin)
                .WithMany()
                .HasForeignKey(p => p.KlantLoginId);

        }
    }
}
