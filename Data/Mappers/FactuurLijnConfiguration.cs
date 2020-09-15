using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
	public class FactuurLijnConfiguration : IEntityTypeConfiguration<FactuurLijn>
	{
		

		public void Configure(EntityTypeBuilder<FactuurLijn> builder)
		{
			builder.ToTable("tblFACL_FactuurLijnen");
			builder.Property(x => x.Id).HasColumnName("tFACLId");
			builder.Property(x => x.FactuurNummer).HasColumnName("tFACL_FacId");
			builder.Property(x => x.ProductId).HasColumnName("tFACLProId");
			builder.Property(x => x.ProductOmschrijving).HasColumnName("tFACLProOmschrijving");
			builder.Property(x => x.Aantal).HasColumnName("tFACLAantal");
			builder.Property(x => x.Prijs).HasColumnName("tFACLPrijs");
			builder.Property(x => x.BtwPercentage).HasColumnName("tFACLBTWPercentage");
			builder.Property(x => x.KlantReferentie).HasColumnName("tFACLKlantReferentie");
			builder.Property(x => x.Opmerking).HasColumnName("tFACLOpmerking");
			builder.Property(x => x.SerieNummer).HasColumnName("tFACLSerieNummer");
			builder.Property(x => x.AfleverWeek).HasColumnName("tFACLAfleverweek");
			builder.Property(x => x.AfleverJaar).HasColumnName("tFACLAfleverjaar");
			builder.Property(x => x.ZendNummer).HasColumnName("tFACLZENNummer");
			//builder.Property(x => x.ZendLijnNummer).HasColumnName("tFACLZENLId");
			builder.Property(x => x.DatumInbreng).HasColumnName("tFACLDatumInbreng");
			
			
		}
	}
}
