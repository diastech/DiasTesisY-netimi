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
    public class TicketNotesConfiguration : IEntityTypeConfiguration<TicketNotes>
    {
        public void Configure(EntityTypeBuilder<TicketNotes> builder)
        {
            builder.ToTable("Ticket_Notes");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.notes)
                .HasColumnName("notes");

            builder.Property(e => e.notesAddedUser)
                .HasColumnName("notesAddedUser");

            builder.Property(e => e.addedTime)
                .HasColumnName("addedTime");

            builder.Property(x => x.ticketId)
                .HasColumnName("ticketId");

            builder.Property(x => x.attachmentId)
                .HasColumnName("attachmentId");
        }
    }
}