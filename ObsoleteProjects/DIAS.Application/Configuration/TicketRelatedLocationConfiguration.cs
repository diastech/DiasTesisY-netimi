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
    public class TicketRelatedLocationConfiguration : IEntityTypeConfiguration<TicketRelatedLocation>
    {
        public void Configure(EntityTypeBuilder<TicketRelatedLocation> builder)
        {
            builder.ToTable("Ticket_Related_Location");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.ticketId)
                .HasColumnName("ticketId");

            builder.Property(e => e.ticketLocationId)
                .HasColumnName("ticketLocationId");


        }
    }
}