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
            builder.Property(x => x.BestellingId).HasColumnName("tOBESL_BesNummer");
            builder.Property(x => x.ProductId).HasColumnName("tOBESLPROId");
            builder.Property(x => x.Aantal).HasColumnName("tOBESLAantal");
            builder.Property(x => x.Prijs).HasColumnName("tOBESLPrijs");
            builder.Property(x => x.BtwPerc).HasColumnName("tOBESLBtwPerc");
            builder.Property(x => x.KlantId).HasColumnName("tOBESLKlaId");
            builder.Property(x => x.DatumInbreng).HasColumnName("tBESLDatumInbreng");


            
            
        }
    }
}
