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
    public class PeriodicTicketDefinitionsConfiguration : IEntityTypeConfiguration<PeriodicTicketDefinitions>
    {
        public void Configure(EntityTypeBuilder<PeriodicTicketDefinitions> builder)
        {
            builder.ToTable("Per_Ticket_Def");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.ticketPeriodName)
                .HasColumnName("ticketPeriodName");

            builder.Property(e => e.startDateTime)
                .HasColumnName("startDateTime");

            builder.Property(e => e.endDateTime)
                .HasColumnName("endDateTime");

            builder.Property(x => x.priority)
                .HasColumnName("priority");

            builder.Property(x => x.locationId)
                .HasColumnName("locationId");

            builder.Property(x => x.frequency)
                .HasColumnName("frequency");

            builder.Property(x => x.reasonId)
               .HasColumnName("reasonId");
        }
    }
}