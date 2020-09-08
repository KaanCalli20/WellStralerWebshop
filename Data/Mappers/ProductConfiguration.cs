using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("tblPRO_Producten");
			builder.Property(x => x.Id).HasColumnName("tPROID");
			builder.Property(x => x.Afk).HasColumnName("tPROAfk");
			builder.Property(x => x.NaamNL).HasColumnName("tPRONedNaam");
			builder.Property(x => x.OmschrijvingNL).HasColumnName("tPRONedOmschrijving");
			builder.Property(x => x.NaamEN).HasColumnName("tPROEngNaam");
			builder.Property(x => x.OmschrijvingEN).HasColumnName("tPROEngOmschrijving");
			builder.Property(x => x.NaamFR).HasColumnName("tPROFraNaam");
			builder.Property(x => x.OmschrijvingFR).HasColumnName("tPROFraOmschrijving");
			builder.Property(x => x.Prijs).HasColumnName("tPROPrijs");
			builder.Property(x => x.Prijs1).HasColumnName("tPROPrijs1");
			builder.Property(x => x.Prijs2).HasColumnName("tPROPrijs2");
			builder.Property(x => x.PrijsGekoppeld).HasColumnName("tPROPrijsGekoppeld");
			builder.Property(x => x.BtwPerc).HasColumnName("tPROBtwPerc");
			builder.Property(x => x.Stock).HasColumnName("tPROStock");
			builder.Property(x => x.StockBestelId).HasColumnName("tPROStockBesteld");
			builder.Property(x => x.Typekorting).HasColumnName("tPROTypeKorting");
			builder.Property(x => x.SoortProduct).HasColumnName("tPROSrtPROId");
			builder.Property(x => x.Verwijderd).HasColumnName("tPROVerwijderd");

			builder.HasOne(x => x.ProductType)
				.WithMany()
				.HasForeignKey("tPRO_TypeId");
			
			}

        
    }
}
