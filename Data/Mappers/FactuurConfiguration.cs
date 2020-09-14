using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class FactuurConfiguration : IEntityTypeConfiguration<Factuur>
    {
        public void Configure(EntityTypeBuilder<Factuur> builder)
        {
			builder.ToTable("tblFAC_Facturen");
			builder.Property(x => x.Id).HasColumnName("tFacId");
			builder.Property(x => x.FactuurNummer).HasColumnName("tFACNummer");
			builder.Property(x => x.Type).HasColumnName("tFACType");
			builder.Property(x => x.Datum).HasColumnName("tFACDatum");
			builder.Property(x => x.BestelDatum).HasColumnName("tFACBesDatum");
			builder.Property(x => x.FacAutoAangemaakt).HasColumnName("tFACAutogemaakt");
			builder.Property(x => x.DatumInbreng).HasColumnName("tFACDatumInbreng");
			builder.Property(x => x.KllId).HasColumnName("tFACUsrIdInbreng");
			builder.Property(x => x.Commentaar).HasColumnName("tFACCommentaar");
			builder.Property(x => x.Doorgestuurd).HasColumnName("tFACDoorgestuurd");
			builder.Property(x => x.KlantId).HasColumnName("tFACKLAId");
			builder.Property(x => x.KlantFirma).HasColumnName("tFACKLAFirma");
			builder.Property(x => x.KlantNaam).HasColumnName("tFACKLANaam");
			builder.Property(x => x.KlantStraat).HasColumnName("tFACKLAStraat");
			builder.Property(x => x.KlantPostcode).HasColumnName("tFACKLAPostcode");
			builder.Property(x => x.KlantLand).HasColumnName("tFACKLALand");
			builder.Property(x => x.KlantFactuurStraat).HasColumnName("tFACKLAFACStraat");
			builder.Property(x => x.KlantFactuurPostcode).HasColumnName("tFACKLAFACPostcode");
			builder.Property(x => x.KlantBtwPlichtig).HasColumnName("tFACKLABTWPlicht");
			builder.Property(x => x.KlantBtwNr).HasColumnName("tFACKLABTWNr");
			builder.Property(x => x.KlantTaal).HasColumnName("tFACKLATaal");
			builder.Property(x => x.LeverKlantId).HasColumnName("tFACLEVKLAId");

			builder.HasMany(t => t.FactuurLijnen)
				.WithOne(p=>p.Factuur).IsRequired()
				.HasForeignKey(t => t.FactuurNummer)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(m => m.Klant)
				.WithMany()
				.HasForeignKey(m => m.KlantId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(m => m.LeverKlant)
				.WithMany()
				.HasForeignKey(m => m.LeverKlantId)
				.OnDelete(DeleteBehavior.Restrict);
		}
    }
}
