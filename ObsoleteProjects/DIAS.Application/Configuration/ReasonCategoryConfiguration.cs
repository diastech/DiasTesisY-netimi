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
    public class ReasonCategoryConfiguration : IEntityTypeConfiguration<ReasonCategory>
    {
        public void Configure(EntityTypeBuilder<ReasonCategory> builder)
        {
            builder.ToTable("Reason_Category");

            builder.HasKey(x => x.id);

            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.reasonCategoryName)
                .HasColumnName("reasonCategoryName");

            builder.Property(e => e.reasonCategoryDescription)
                .HasColumnName("reasonCategoryDescription");

            builder.Property(e => e.reasonCategoryHierarchy)
                .HasColumnName("reasonCategoryHierarchy");

            builder.Property(e => e.reasonCategoryParentId)
                .HasColumnName("reasonCategoryParentId");
        }
    }
}