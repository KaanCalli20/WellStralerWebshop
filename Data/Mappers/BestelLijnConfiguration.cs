using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class BestelLijnConfiguration : IEntityTypeConfiguration<BestelLijn>
    {


        public void Configure(EntityTypeBuilder<BestelLijn> builder)
        {
           

            builder.ToTable("tblBESL_BestelLijnen");
            builder.Property(x => x.Id).HasColumnName("tBESLId");
            builder.Property(x => x.BestellingId).HasColumnName("tBESL_BesNummer");
            builder.Property(x => x.ProductId).HasColumnName("tBESLPROId");
            builder.Property(x => x.ProductOmschrijving).HasColumnName("tBESLProOmschrijving");
            builder.Property(x => x.Aantal).HasColumnName("tBESLAantal");
            builder.Property(x => x.AantalKlaar).HasColumnName("tBESLAantalKlaar");
            builder.Property(x => x.AantalGeleverd).HasColumnName("tBESLAantalGeleverd");
            builder.Property(x => x.Prijs).HasColumnName("tBESLPrijs");
            builder.Property(x => x.BtwPerc).HasColumnName("tBESLBtwPerc");
            builder.Property(x => x.Opmerking).HasColumnName("tBESLOpmerking");
            builder.Property(x => x.KlantReferentie).HasColumnName("tBESLKlantReferentie");
            builder.Property(x => x.Afleverweek).HasColumnName("tBESLAfleverweek");
            builder.Property(x => x.Afleverjaar).HasColumnName("tBESLAfleverjaar");
            builder.Property(x => x.Afgewerkt).HasColumnName("tBESLAfgewerkt");
            builder.Property(x => x.Geblokkeerd).HasColumnName("tBESLGeblokkeerd");
            builder.Property(x => x.DatumInbreng).HasColumnName("tBESLDatumInbreng");
            //builder.Property(x => x.CreatedBy).HasColumnName("tBESLUsrIdInbreng");
            builder.Property(x => x.DatumWijziging).HasColumnName("tBESLDatumWijziging");
            //builder.Property(x => x.ModifiedBy).HasColumnName("tBESLUsrIdWijziging");
            builder.Property(x => x.HoofdProdBestelId).HasColumnName("tBESLHoofdProdBeslId");
            
        }
    }
}
