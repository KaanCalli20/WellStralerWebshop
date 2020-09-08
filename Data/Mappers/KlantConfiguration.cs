using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class KlantConfiguration : IEntityTypeConfiguration<Klant>
    {
        public void Configure(EntityTypeBuilder<Klant> builder)
        {
			builder.ToTable("tblKLA_Klanten");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).HasColumnName("tKLAId");
			builder.Property(x => x.Taal).HasColumnName("tKLAtaal");
			builder.Property(x => x.Firma).HasColumnName("tKLAFirma");
			builder.Property(x => x.Titel).HasColumnName("tKLATitel");
			builder.Property(x => x.Naam).HasColumnName("tKLANaam");
			builder.Property(x => x.Voornaam).HasColumnName("tKLAVoornaam");
			builder.Property(x => x.Btwnummer).HasColumnName("tKLABTWNummer");
			builder.Property(x => x.Straat).HasColumnName("tKLAStraat");
			builder.Property(x => x.Land).HasColumnName("tKLALand");
			builder.Property(x => x.Postcode).HasColumnName("tKLAPostcode");
			builder.Property(x => x.FacturatieAdres).HasColumnName("tKLAFacturatieStraat");
			builder.Property(x => x.FacturatiePostcode).HasColumnName("tKLAFacturatiePostcode");
			builder.Property(x => x.Telefoon).HasColumnName("tKLATelefoon");
			builder.Property(x => x.Fax).HasColumnName("tKLAFax");
			builder.Property(x => x.Email).HasColumnName("tKLAEmail");
			builder.Property(x => x.Website).HasColumnName("tKLAWebsite");
			builder.Property(x => x.Mobile).HasColumnName("tKLAMobile");
			builder.Property(x => x.Leverdag).HasColumnName("tKLALeverdag");
			builder.Property(x => x.LaatsteLevering).HasColumnName("tKLALaatsteLevering");
			builder.Property(x => x.Korting1).HasColumnName("tKLAKorting1");
			builder.Property(x => x.Korting2).HasColumnName("tKLAKorting2");
			builder.Property(x => x.DatumInbreng).HasColumnName("tKLADatumInbreng");
			builder.Property(x => x.UserIdInbreng).HasColumnName("tKLAUSRIdInbreng");
			builder.Property(x => x.DatumWijziging).HasColumnName("tKLADatumWijziging");
			builder.Property(x => x.UserIdWijziging).HasColumnName("tKLAUSRIdWijziging");
			builder.Property(x => x.BtwPlichtig).HasColumnName("tKLABTWPlichtig");
			builder.Property(x => x.OpenstaandeSaldo).HasColumnName("tKLAOpenstaandSaldo");
			builder.Property(x => x.OmzetVJ).HasColumnName("tKLAOmzetVJ");
			builder.Property(x => x.Omzet).HasColumnName("tKLAOmzet");



		}
	}
}
