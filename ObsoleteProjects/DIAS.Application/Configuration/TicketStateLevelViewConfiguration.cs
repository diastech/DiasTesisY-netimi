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
    public class TicketStateLevelViewConfiguration : IEntityTypeConfiguration<TicketStateLevelView>
    {
        public void Configure(EntityTypeBuilder<TicketStateLevelView> builder)
        {
            builder.HasNoKey();
            builder.ToView("vw_TicketStateLevel");
        }
    }
}