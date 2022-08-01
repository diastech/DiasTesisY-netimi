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
    public class UserPageConfiguration : IEntityTypeConfiguration<UserPage>
    {
        public void Configure(EntityTypeBuilder<UserPage> builder)
        {
            builder.ToTable("UserPage");

            builder.Property(x => x.Id)
                .HasColumnName("id");

            builder.Property(e => e.PageId)
                .HasColumnName("applicationPageId");

            builder.Property(e => e.UserId)
                .HasColumnName("applicationUserId");

            builder.Property(x => x.Created)
                .HasColumnName("createdDate");

            builder.Property(x => x.CreatedBy)
                .HasColumnName("createdBy");

            builder.Property(x => x.LastModified)
                .HasColumnName("lastModifiedDate");

            builder.Property(x => x.LastModifiedBy)
                .HasColumnName("lastModifiedBy");

            builder.Property(x => x.IsActive)
                .HasColumnName("isActive")
                .HasDefaultValue(true);
        }
    }
}