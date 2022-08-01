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
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.locationName)
                .HasColumnName("locationName");

            builder.Property(e => e.locationNumber)
                .HasColumnName("locationNumber");

            builder.Property(e => e.locationDescription)
                .HasColumnName("locationDescription");

            builder.Property(x => x.locationLangLong)
                .HasColumnName("locationLangLong");

            builder.Property(x => x.locationHierarchy)
                .HasColumnName("locationHierarchy");

            builder.Property(x => x.locationParentId)
                .HasColumnName("locationParentId");
        }
    }
}