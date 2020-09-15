using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class BestellingConfiguration : IEntityTypeConfiguration<Bestelling>
    {

        public void Configure(EntityTypeBuilder<Bestelling> builder)
        {
            builder.ToTable("tblBES_Bestelling");
            builder.Property(x => x.Id).HasColumnName("tBESNummer");
            builder.Property(x => x.Datum).HasColumnName("tBESDatum");
            builder.Property(x => x.KlantId).HasColumnName("tBESKLAId");
            builder.Property(x => x.Afleverdag).HasColumnName("tBESAfleverdag");
            builder.Property(x => x.DatumInbreng).HasColumnName("tBESDatumInbreng");
            builder.Property(x => x.DatumWijziging).HasColumnName("tBESDatumWijziging");
            builder.Property(x => x.KlantFirma).HasColumnName("tBESKLAFirma");
            builder.Property(x => x.KlantNaam).HasColumnName("tBESKLANaam");
            builder.Property(x => x.KlantStraat).HasColumnName("tBESKLAStraat");
            builder.Property(x => x.KlantPostcode).HasColumnName("tBESKLAPostcode");
            builder.Property(x => x.KlantLand).HasColumnName("tBESKLALand");
            builder.Property(x => x.KlantLeverFirma).HasColumnName("tBESKLALEVFirma");
            builder.Property(x => x.KlantLeverNaam).HasColumnName("tBESKLALEVNaam");
            builder.Property(x => x.KlantLevStraat).HasColumnName("tBESKLALEVStraat");
            builder.Property(x => x.KlantLevPostcode).HasColumnName("tBESKLALEVPostcode");
            builder.Property(x => x.KlantLevLand).HasColumnName("tBESKLALEVLand");
            builder.Property(x => x.KlantFacStraat).HasColumnName("tBESKLAFACStraat");
            builder.Property(x => x.KlantFacPostcode).HasColumnName("tBESKLAFACPostcode");
            builder.Property(x => x.KlantBtwNr).HasColumnName("tBESKLABTWNr");
            builder.Property(x => x.KlantTaal).HasColumnName("tBESKLATaal");
            builder.Property(x => x.KlantLevTaal).HasColumnName("tBESKLALEVTaal");
            builder.Property(x => x.KlantBtwPlicht).HasColumnName("tBESKLABTWPlicht");
            builder.Property(x => x.Geblokkeerd).HasColumnName("tBESGeblokkeerd");
            builder.Property(x => x.Afgewerkt).HasColumnName("tBESAfgewerkt");
        }
    }
}
