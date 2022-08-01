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
    public class TicketHistoryConfiguration : IEntityTypeConfiguration<TicketHistory>
    {
        public void Configure(EntityTypeBuilder<TicketHistory> builder)
        {
            builder.ToTable("Ticket_History");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.ticketId)
                .HasColumnName("ticketId");

            builder.Property(e => e.ticketHistoryType)
                .HasColumnName("ticketHistoryType");

            builder.Property(e => e.ticketHistoryInsertDate)
                .HasColumnName("ticketHistoryInsertDate");

            builder.Property(x => x.ticketHistoryInsertUserId)
                .HasColumnName("ticketHistoryInsertUserId");

            builder.Property(x => x.previousStatusId)
                .HasColumnName("previousStatusId");

            builder.Property(x => x.nextStatusId)
                .HasColumnName("nextStatusId");

            builder.Property(x => x.activityStartTime)
                .HasColumnName("activityStartTime");

            builder.Property(x => x.activityEndTime)
                .HasColumnName("activityEndTime");

            builder.Property(x => x.locationId)
                .HasColumnName("locationId");

            builder.Property(x => x.previousAssignmentUserId)
                .HasColumnName("previousAssignmentUserId");

            builder.Property(x => x.nextAssignmentUserId)
                .HasColumnName("nextAssignmentUserId");

            builder.Property(x => x.previousAssignmentGroupId)
                .HasColumnName("previousAssignmentGroupId");

            builder.Property(x => x.nextAssignmentGroupId)
               .HasColumnName("nextAssignmentGroupId");
        }
    }
}