using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellStralerWebshop.Models.Domain;

namespace WellStralerWebshop.Data.Mappers
{
    public class ParameterConfiguration : IEntityTypeConfiguration<Parameters>
    {
        public void Configure(EntityTypeBuilder<Parameters> builder)
        {
            builder.ToTable("tblPAR_Parameters");
            builder.Property(x => x.ParameterId).HasColumnName("tParId");
            builder.HasKey(m => m.ParameterId);
            builder.Property(x => x.ParameterTable).HasColumnName("tParTable");
            builder.Property(x => x.ParameterKey).HasColumnName("tParKey");
            builder.Property(x => x.ParameterWaarde).HasColumnName("tParInt1");
            builder.Property(x => x.ParameterKleinerDan).HasColumnName("tParInt2");
            builder.Property(x => x.ParameterGelijkAan).HasColumnName("tParInt3");
            builder.Property(x => x.ParameterGroterDan).HasColumnName("tParInt4");
            builder.Property(x => x.Parameter5).HasColumnName("tParInt5");
            builder.Property(x => x.ParameterBeschrijving1).HasColumnName("tParChar1");
            builder.Property(x => x.ParameterBeschrijving2).HasColumnName("tParChar2");
            builder.Property(x => x.ParameterBeschrijving3).HasColumnName("tParChar3");
            builder.Property(x => x.ParameterBeschrijving4).HasColumnName("tParChar4");
            builder.Property(x => x.ParameterBeschrijving5).HasColumnName("tParChar5");


        }
    }
}
