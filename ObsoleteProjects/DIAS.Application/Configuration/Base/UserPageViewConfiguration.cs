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
    public class UserPageViewConfiguration : IEntityTypeConfiguration<UserPageView>
    {
        public void Configure(EntityTypeBuilder<UserPageView> builder)
        {
            builder.HasNoKey();
            builder.ToView(nameof(UserPageView));
            //builder.Property(v => v.UserId).HasColumnName("UserId");
        }
    }
}