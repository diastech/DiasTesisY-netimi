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
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.ticketInsertUser)
                .HasColumnName("ticketInsertUser");

            builder.Property(e => e.ticketOwner)
                .HasColumnName("ticketOwner");

            builder.Property(e => e.ticketDescription)
                .HasColumnName("ticketDescription");

            builder.Property(x => x.ticketOpenedTime)
                .HasColumnName("ticketOpenedTime");

            builder.Property(x => x.ticketStatus)
                .HasColumnName("ticketStatus");

            builder.Property(x => x.periodicTicketId)
                .HasColumnName("periodicTicketId");

            builder.Property(x => x.responsibleUserId)
                .HasColumnName("responsibleUserId");

            builder.Property(x => x.responsibleAssignmentGroupId)
                .HasColumnName("responsibleAssignmentGroupId");

            builder.Property(x => x.reasonId)
                .HasColumnName("reasonId");

            builder.Property(x => x.priority)
                .HasColumnName("priority");

            builder.Property(x => x.locationId)
                .HasColumnName("locationId");

            builder.Property(x => x.basicTicketsId)
                .HasColumnName("basicTicketsId");
        }
    }
}