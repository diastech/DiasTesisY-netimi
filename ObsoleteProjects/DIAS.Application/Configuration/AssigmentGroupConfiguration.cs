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
    public class AssigmentGroupConfiguration : IEntityTypeConfiguration<AssigmentGroup>
    {
        public void Configure(EntityTypeBuilder<AssigmentGroup> builder)
        {
            builder.ToTable("Asg_Group");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.asgGroupName)
                .HasColumnName("asgGroupName");

            builder.Property(e => e.asgGroupManagerId)
                .HasColumnName("asgGroupManagerId");

            builder.Property(e => e.reasonId)
                .HasColumnName("reasonId");

            builder.Property(x => x.reasonCatId)
                .HasColumnName("reasonCatId");

            

           
        }
    }
}