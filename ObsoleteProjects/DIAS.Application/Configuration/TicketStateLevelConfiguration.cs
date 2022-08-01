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
    public class TicketStateLevelConfiguration : IEntityTypeConfiguration<TicketStateLevel>
    {
        public void Configure(EntityTypeBuilder<TicketStateLevel> builder)
        {
            builder.ToTable("Ticket_State_Level");

            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.ticketStateSource)
                .HasColumnName("ticketStateSource");

            builder.Property(e => e.ticketStateDestination)
               .HasColumnName("ticketStateDestination");
        }
    }
}