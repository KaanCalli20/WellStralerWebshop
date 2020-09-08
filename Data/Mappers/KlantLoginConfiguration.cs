using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class KlantLoginConfiguration : IEntityTypeConfiguration<KlantLogin>
	{
		

		public void Configure(EntityTypeBuilder<KlantLogin> builder)
		{
			builder.ToTable("tblKLL_KlaLogin");
			builder.Property(x => x.Id).HasColumnName("tKLLId");
			builder.Property(x => x.Naam).HasColumnName("tKLLNaam");
			builder.Property(x => x.Voornaam).HasColumnName("tKLLVoornaam");
			builder.Property(x => x.Gebruikersnaam).HasColumnName("tKLLLogin");
			builder.Property(x => x.Paswoord).HasColumnName("tKLLPaswoord");
			builder.Property(x => x.Actief).HasColumnName("tKLLActief");
			builder.Property(x => x.Wijzigbaar).HasColumnName("tKLLWijzigbaar");
			builder.Property(x => x.KlantId).HasColumnName("tKLL_tKLAId");

			builder.HasOne(p => p.klant)
				.WithMany()
				.HasForeignKey(p => p.KlantId);

		}
	}
}
