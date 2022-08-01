using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIAS.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIAS.Domain.Configuration.Base
{
    public class ApplicationPageConfiguration : IEntityTypeConfiguration<ApplicationPage>
    {
        public void Configure(EntityTypeBuilder<ApplicationPage> builder)
        {
            builder.ToTable("Page");

            builder.Property(x => x.Id)
                .HasColumnName("id");
            
            builder.Property(x => x.Order)
                .HasColumnName("order")
                .IsRequired();            
            
            builder.Property(x => x.Level)
                .HasColumnName("level")
                .IsRequired();

            builder.Property(x => x.ParentId)
                .HasColumnName("parentId");
            
            builder.Property(x => x.Text)
                .HasColumnName("text")
                .IsRequired();

            builder.Property(x => x.Path)
                .HasColumnName("path");

            builder.Property(x => x.Icon)
                .HasColumnName("icon");
            
            builder.Property(x => x.Image)
                .HasColumnName("image")
                .HasColumnType("text");

            builder.Property(x => x.Expanded)
                .HasColumnName("expanded")
                .HasDefaultValue(false);
            
            builder.Property(x => x.IsActive)
                .HasColumnName("isActive")
                .HasDefaultValue(true);
        }
    }
}