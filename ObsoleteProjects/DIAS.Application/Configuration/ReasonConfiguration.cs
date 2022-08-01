using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIAS.Domain.Configuration.Base
{
    public class ReasonConfiguration : IEntityTypeConfiguration<Reason>
    {
        public void Configure(EntityTypeBuilder<Reason> builder)
        {
            builder.ToTable("Reason");

            builder.HasKey(x => x.id);

            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.reasonName)
                .HasColumnName("reasonName");

            builder.Property(e => e.reasonDescription)
                .HasColumnName("reasonDescription");

            builder.Property(e => e.responseTime)
                .HasColumnName("responseTime");

            builder.Property(e => e.resolutionTime)
                .HasColumnName("resolutionTime");

            builder.Property(e => e.reasonCategoryId)
              .HasColumnName("reasonCategoryId");

            builder.Property(e => e.reasonCode)
            .HasColumnName("reasonCode");
        }
    }
}