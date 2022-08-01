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
    public class AssigmentGroupAuthLocationConfiguration : IEntityTypeConfiguration<AssigmentGroupAuthLocation>
    {
        public void Configure(EntityTypeBuilder<AssigmentGroupAuthLocation> builder)
        {
            builder.ToTable("Asg_Group_Auth_Loc");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.asgId)
                .HasColumnName("asgId");

            builder.Property(e => e.locationId)
                .HasColumnName("locationId");

           
        }
    }
}